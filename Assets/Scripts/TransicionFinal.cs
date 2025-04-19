using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TransicionFinal : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;

    void Start()
    {
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
        TransicionEscena transicion = FindFirstObjectByType<TransicionEscena>();
        if (transicion != null)
        {
            transicion.IrAEscena("MenuInicio");
        }
        else
        {
            SceneManager.LoadScene("MenuInicio");
        }
    }
}
