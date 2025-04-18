using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneEndManager : MonoBehaviour
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
            transicion.IrASiguienteEscena();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
