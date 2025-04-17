using UnityEngine;
using TMPro;

/*Autora: Daiana Andrea Armenta Maya
    * Descripción: Clase que gestiona la activación de un panel de preguntas al entrar en un área específica.
    * Muestra un marcador cuando el jugador está cerca y activa un panel de preguntas al presionar "E".
    */
public class TriggerPreguntas : MonoBehaviour
{
    [SerializeField] private GameObject marker;  // El marcador que aparece cuando el jugador está cerca
    [SerializeField] private GameObject preguntasPanel; // El panel de preguntas que aparecerá al presionar "E"
    [SerializeField] private Pregunta preguntaAsociada; // La pregunta asociada a este checkpoint
    [SerializeField] private AudioClip sonidoCheckpoint;

    private AudioSource audioSource;
    private bool isPlayerInRange;  // Indica si el jugador está en el rango
    private bool preguntaContestada = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        // Si el jugador está en el rango y presiona la tecla "E"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)&& !preguntaContestada)
        {
            // Mostrar el panel de preguntas
            Debug.Log("Tecla 'E' presionada, mostrando panel de preguntas.");
            MostrarPanelPreguntas();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el jugador entró en el área
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (!preguntaContestada)
            {
                marker.SetActive(true);  // Mostrar el marcador
                Debug.Log("Marcador activado.");
                audioSource.PlayOneShot(sonidoCheckpoint); // Reproducir el sonido del checkpoint
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verificar si el jugador salió del área
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (marker != null)  // Verificar si el objeto existe
            {
                marker.SetActive(false);  // Ocultar el marcador
                Debug.Log("Marcador desactivado.");
            }
            if (preguntasPanel != null)  // Verificar si el objeto existe
            {
                preguntasPanel.SetActive(false); // Ocultar el panel de preguntas
                Debug.Log("Panel desactivado.");
            }
        }
    }

    private void MostrarPanelPreguntas()
    {
        if (PreguntaManager.instance == null)
        {
            Debug.LogError("PreguntaManager no está configurado como singleton.");
            return;
        }

        if (preguntasPanel != null && preguntaAsociada != null)
        {
            preguntasPanel.SetActive(true);  // Activar el panel de preguntas
            PreguntaManager.instance.MostrarPregunta(preguntaAsociada, MarcarComoContestada);  // Mostrar la pregunta
            Debug.Log("Panel de preguntas activado.");
        }
        else
        {
            Debug.LogError("No se ha asignado el panel de preguntas o la pregunta asociada en el Inspector.");
        }

    }

    public void MarcarComoContestada()
    {
        preguntaContestada = true;
        marker.SetActive(false);  // Ocultar el marcador
        Debug.Log("Ya se contestó la pregunta tonoto");
    }
}



