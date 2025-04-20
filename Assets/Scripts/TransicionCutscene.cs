using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneEndManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    private Animator personajeAnimator;

    void Start()
    {
        // Buscar el objeto "personaje" por nombre
        GameObject personaje = GameObject.Find("personaje");
        if (personaje != null)
        {
            personajeAnimator = personaje.GetComponent<Animator>();
            if (personajeAnimator != null)
            {
                personajeAnimator.enabled = true; // Desactiva animaciones durante la cutscene
            }
            else
            {
                Debug.LogWarning("El objeto 'personaje' no tiene componente Animator.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto llamado 'personaje'.");
        }

        if (timeline != null)
        {
            timeline.stopped += OnTimelineFinished;
        }
        else
        {
            Debug.LogWarning("No se asignó el PlayableDirector (Timeline) en el Inspector.");
        }
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // Reactivar el Animator al terminar la cutscene
        if (personajeAnimator != null)
        {
            personajeAnimator.enabled = false;
        }

        TransicionEscena transicion = FindFirstObjectByType<TransicionEscena>();
        if (transicion != null)
        {
            transicion.IrASiguienteEscena();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
