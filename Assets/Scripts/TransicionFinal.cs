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
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
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
