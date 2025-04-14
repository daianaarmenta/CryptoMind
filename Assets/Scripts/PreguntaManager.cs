using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public void MostrarPregunta(Pregunta pregunta)
    {
        if (panelPregunta != null)
        {
            panelPregunta.SetActive(true);
            textoPregunta.text = pregunta.textoPregunta;

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
        if (respuestaIndex == pregunta.respuestaCorrecta)
        {
            Debug.Log("✅ ¡Respuesta correcta!");
            GameManager.Instance.SumarPuntos(100);
        }
        else
        {
            Debug.Log("❌ Respuesta incorrecta.");
            if (SaludPersonaje.instance != null)    
            {
                SaludPersonaje.instance.PerderVida();
            }
            else
            {
                Debug.LogError("No se encontró una instancia de SaludPersonaje.");
            }
        }



        panelPregunta.SetActive(false);
    }
}
