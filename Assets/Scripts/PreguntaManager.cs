using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

/* Autora: Daiana Andrea Armenta Maya
 * Descripción: Clase que gestiona la lógica de las preguntas y respuestas en el juego.
 * Permite mostrar preguntas, verificar respuestas y gestionar el puntaje del jugador.
 */
public class PreguntaManager : MonoBehaviour
{
    public static PreguntaManager instance;

    [SerializeField] private TextMeshProUGUI textoPregunta;
    [SerializeField] private Button[] botonesRespuestas;
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private GameObject mensajeRespuesta;
    [SerializeField] private TextMeshProUGUI mensajeTexto;
    [SerializeField] private float tiempoMensaje =2f;

    private System.Action callbackFinPregunta;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipCorrecto;
    [SerializeField] private AudioClip audioClipIncorrecto;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("PreguntaManager configurado como singleton.");
        }
        else
        {
            Debug.LogWarning("Se intentó crear una segunda instancia de PreguntaManager. Destruyendo duplicado.");
            Destroy(gameObject);
        }
    }

    public void MostrarPregunta(Pregunta pregunta, System.Action callbackFin)
    {
        callbackFinPregunta = callbackFin;
        if (panelPregunta != null)
        {
            panelPregunta.SetActive(true);
            textoPregunta.text = pregunta.textoPregunta;
            Time.timeScale = 0f; // Pausar el juego

            for (int i = 0; i < botonesRespuestas.Length; i++)
            {
                if (i < pregunta.respuestas.Length)
                {
                    botonesRespuestas[i].gameObject.SetActive(true);

                    // ✅ Usando Text (Legacy)
                    Text textoBoton = botonesRespuestas[i].GetComponentInChildren<Text>();
                    if (textoBoton != null)
                    {
                        textoBoton.text = pregunta.respuestas[i];
                    }

                    botonesRespuestas[i].onClick.RemoveAllListeners();
                    int respuestaIndex = i;

                    botonesRespuestas[i].onClick.AddListener(() => ComprobarRespuesta(pregunta, respuestaIndex));
                }
                else
                {
                    botonesRespuestas[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("❌ No se ha asignado el panel de la pregunta en el Inspector.");
        }
    }

    private void ComprobarRespuesta(Pregunta pregunta, int respuestaIndex)
    {
        string respuestaCorrecta = pregunta.respuestas[pregunta.respuestaCorrecta];
        if (respuestaIndex == pregunta.respuestaCorrecta)
        {
            Debug.Log("✅ ¡Respuesta correcta!");
            GameManager.Instance.SumarMonedas(100); // ✅ Puntaje, no monedas
            MostrarMensaje("Correct! +100 coins", Color.green);
            audioSource.PlayOneShot(audioClipCorrecto);
        }
        else
        {
            Debug.Log("❌ Respuesta incorrecta.");
            MostrarMensaje("Incorrect!\n-1 life\n\nCorrect answer:" + respuestaCorrecta , Color.red);
            audioSource.PlayOneShot(audioClipIncorrecto);
            if (SaludPersonaje.instance != null)
            {
                SaludPersonaje.instance.PerderVida();
            }
            else
            {
                Debug.LogError("No se encontró una instancia de SaludPersonaje.");
            }
        }

        StartCoroutine(OcultarMensaje());
    }

    private void MostrarMensaje(string mensaje, Color color)
    {
        mensajeRespuesta.SetActive(true);
        Debug.Log("panel de respuesta activo");
        mensajeTexto.text = mensaje;
        mensajeTexto.color = color;
        Debug.Log("🟢 Mostrando mensaje: " + mensaje);
    }

    private System.Collections.IEnumerator OcultarMensaje()
    {
        yield return new WaitForSecondsRealtime(tiempoMensaje);

        mensajeRespuesta.SetActive(false);
        panelPregunta.SetActive(false);
        Time.timeScale = 1f; // Reanudar el juego
        callbackFinPregunta?.Invoke(); // Llamar al callback al finalizar la pregunta
        callbackFinPregunta = null; // Limpiar el callback para evitar llamadas múltiples

        // ✅ Aumentar el conteo de checkpoints desde el alien
        alien controlador = Object.FindFirstObjectByType<alien>();
        if (controlador != null)
        {
            controlador.AumentarCheckpoints();
        }
        else
        {
            Debug.LogError("No se encontró una instancia de alien.");
        }
    }

}
