using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* Autora: Daiana Andrea Armenta Maya
    * Descripción: Clase que gestiona la lógica de las preguntas y respuestas en el juego.
    * Permite mostrar preguntas, verificar respuestas y gestionar el puntaje del jugador.
*/
public class PreguntaManager : MonoBehaviour
{
    public static PreguntaManager instance;  // Singleton para acceder al manager desde otros scripts

    [SerializeField] private TextMeshProUGUI textoPregunta;  // Texto de la pregunta
    [SerializeField] private Button[] botonesRespuestas;     // Botones de las respuestas
    [SerializeField] private GameObject panelPregunta;      // Panel de la pregunta

    private void Awake()
    {
        // Configurar el Singleton
        if (instance == null)
        {
            instance = this;
            Debug.Log("PreguntaManager configurado como singleton.");
        }
        else
        {
            Debug.LogWarning("Se intentó crear una segunda instancia de PreguntaManager. Destruyendo duplicado.");
            Destroy(gameObject);  // Evitar duplicados
        }
    }

    // Método para mostrar la pregunta
    public void MostrarPregunta(Pregunta pregunta)
    {
        if (panelPregunta != null)
        {
            panelPregunta.SetActive(true);  // Activar el panel
            textoPregunta.text = pregunta.textoPregunta;  // Mostrar el texto de la pregunta

            // Asignar las respuestas a los botones
            for (int i = 0; i < botonesRespuestas.Length; i++)
            {
                if (i < pregunta.respuestas.Length)  // Asegurarse de que no haya más botones que respuestas
                {
                    botonesRespuestas[i].gameObject.SetActive(true);  // Activar el botón
                    Text textoBoton = botonesRespuestas[i].GetComponentInChildren<Text>();  // Obtener el texto del botón

                    // Limpiar listeners anteriores y asignar uno nuevo
                    botonesRespuestas[i].onClick.RemoveAllListeners();
                    int respuestaIndex = i;  // Capturar el índice para el listener
                    botonesRespuestas[i].onClick.AddListener(() => ComprobarRespuesta(pregunta, respuestaIndex));
                }
                else
                {
                    botonesRespuestas[i].gameObject.SetActive(false);  // Desactivar botones no usados
                }
            }
        }
        else
        {
            Debug.LogError("No se ha asignado el panel de la pregunta en el Inspector.");
        }
    }

    // Método para comprobar la respuesta
    private void ComprobarRespuesta(Pregunta pregunta, int respuestaIndex)
    {
        if (respuestaIndex == pregunta.respuestaCorrecta)
        {
            Debug.Log("¡Respuesta correcta!");
            GameManager.Instance.SumarPuntos(100);
            //SceneManager.LoadScene("Correcto");  // Sumar puntos al GameManager

            // Aquí puedes agregar lógica adicional, como dar puntos o recompensas.
        }
        else
        {
            Debug.Log("Respuesta incorrecta.");
            // Descontar una vida si la respuesta es incorrecta
            if (SaludPersonaje.instance != null)
            {
                SaludPersonaje.instance.PerderVida();
            }
            else
            {
                Debug.LogError("No se encontró una instancia de SaludPersonaje.");
            }

            //SceneManager.LoadScene("Incorrecto");  // Cambiar a la escena de respuesta incorrecta
        }

        // Desactivar el panel después de responder
        panelPregunta.SetActive(false);
    }
}

