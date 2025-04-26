using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TransicionFinal : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    private Animator personajeAnimator;

    void Start()
    {
        GameObject personaje = GameObject.Find("personaje");
        if (personaje != null)
        {
            personajeAnimator = personaje.GetComponent<Animator>();
            if (personajeAnimator != null)
            {
                personajeAnimator.enabled = true;
            }
        }

        if (timeline != null)
        {
            timeline.stopped += OnTimelineFinished;
        }
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        if (personajeAnimator != null)
        {
            personajeAnimator.enabled = false;
        }

        TransicionEscena transicion = FindFirstObjectByType<TransicionEscena>();
        if (transicion != null)
        {
            transicion.IrAEscena("Creditos");
        }
        else
        {
            SceneManager.LoadScene("Creditos");
        }
    }
}
