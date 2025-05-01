using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneEndManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;

    private GameObject personaje;
    private Animator personajeAnimator;
    private Rigidbody2D rb;

    void Start()
    {
        personaje = GameObject.Find("personaje");

        if (personaje != null)
        {
            personajeAnimator = personaje.GetComponent<Animator>();
            rb = personaje.GetComponent<Rigidbody2D>();

            personaje.SetActive(true); // Asegura que no esté desactivado

            if (personajeAnimator != null)
                personajeAnimator.enabled = true;
        }

        if (timeline != null)
        {
            timeline.stopped += OnTimelineFinished;
        }
    }

    private void OnTimelineFinished(PlayableDirector director)
    {

        if (personaje != null)
        {
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.bodyType = RigidbodyType2D.Static; 
            }

            MonoBehaviour[] scripts = personaje.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                if (script != this && script.enabled)
                {
                    script.enabled = false;
                }
            }

            if (personajeAnimator != null)
            {
                personajeAnimator.enabled = false;
            }
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

    private void OnDestroy()
    {
        if (timeline != null)
            timeline.stopped -= OnTimelineFinished;
    }
}
