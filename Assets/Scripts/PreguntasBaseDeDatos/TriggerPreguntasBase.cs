using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
/*Autora: Daiana Andrea Armenta Maya
          Emiliano Plata Cardona
 * Descripción: Clase que gestiona el trigger de preguntas en el juego.
 * Controla la activación del marcador y la interacción del jugador con el checkpoint.
 */
public class TriggerPreguntasBase : MonoBehaviour
{
    [SerializeField] private GameObject marker;
    [SerializeField] private int idPregunta = 1; 
    [SerializeField] private AudioClip sonidoCheckpoint;

    private bool isPlayerInRange = false; 
    private bool preguntaContestada = false;
    private float cooldown = 1f;
    private float tiempoUltimaInteraccion = -10f;

    private AudioSource audioSource;

    private void Start()
    {
      audioSource = GetComponent<AudioSource>();  
    }
    private void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !preguntaContestada)
        {
            if (Time.time - tiempoUltimaInteraccion >= cooldown)
            {
                MostrarPreguntaDesdeServidor();
                tiempoUltimaInteraccion = Time.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !preguntaContestada)
        {
            isPlayerInRange = true;
            if(marker != null)
            {
                marker.SetActive(true);
                //Debug.Log("Jugador entró al checkpoint. Marcador activado.");
                audioSource.PlayOneShot(sonidoCheckpoint);
            }
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !preguntaContestada)
        {
            isPlayerInRange = false;
            if(marker != null)
            {
                marker.SetActive(false);
                //Debug.Log("Jugador salió del checkpoint. Marcador desactivado.");
            }
        }
    }

    private void MostrarPreguntaDesdeServidor()
    {
        if(PreguntaManagerBase.instance == null)
        {
            //Debug.LogError("No se encontró el singleton de PreguntaManagerBase.");
            return;
        }
        //Debug.Log("Solicitando pregunta con ID: " + idPregunta);
        StartCoroutine(CargarYMarcarPregunta(idPregunta));
    }

    private IEnumerator CargarYMarcarPregunta(int id)
    {
        yield return PreguntaManagerBase.instance.CargarPreguntaPorIdJSON(id); // espera para carg
        MarcarComoContestada(); // mark only if loaded
    }

    private void MarcarComoContestada()
    {
        preguntaContestada = true;
        if(marker != null)
        {
            marker.SetActive(false);
        }

        //Debug.Log("Pregunta marcada como contestada y marcador ocultado.");
    }
}