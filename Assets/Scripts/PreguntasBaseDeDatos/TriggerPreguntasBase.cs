using UnityEngine;
using System.Collections;

public class TriggerPreguntasBase : MonoBehaviour
{
    [SerializeField] private GameObject marker;
    [SerializeField] private int idPregunta = 1; 

    private bool isPlayerInRange = false; 
    private bool preguntaContestada = false;

    private void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !preguntaContestada)
        {
            Debug.Log("Tecla 'E' presionada: cargando pregunta del servidor...");
            MostrarPreguntaDesdeServidor();
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
                Debug.Log("Jugador entró al checkpoint. Marcador activado.");
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
                Debug.Log("Jugador salió del checkpoint. Marcador desactivado.");
            }
        }
    }

    private void MostrarPreguntaDesdeServidor()
    {
        if(PreguntaManagerBase.instance == null)
        {
            Debug.LogError("No se encontró el singleton de PreguntaManagerBase.");
            return;
        }
        Debug.Log("Solicitando pregunta con ID: " + idPregunta);
        StartCoroutine(CargarYMarcarPregunta(idPregunta));
    }

    private IEnumerator CargarYMarcarPregunta(int id)
    {
        yield return PreguntaManagerBase.instance.CargarPreguntaPorId(id); // wait for loading
        MarcarComoContestada(); // ✅ mark only if loaded
    }

    private void MarcarComoContestada()
    {
        preguntaContestada = true;
        if(marker != null)
        {
            marker.SetActive(false);
        }

        Debug.Log("Pregunta marcada como contestada y marcador ocultado.");
    }
}