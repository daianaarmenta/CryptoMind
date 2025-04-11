using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class CambioCutscene : MonoBehaviour
{
    [SerializeField] private string escenaSiguiente = "Menu_juego"; // Nombre de la siguiente escena
    private PlayableDirector director;

    private void Start()
    {
        // Obt�n el componente PlayableDirector
        director = GetComponent<PlayableDirector>();

        director.stopped += OnCutsceneEnd;
    }

    // M�todo que se llama cuando la Timeline se detiene
    private void OnCutsceneEnd(PlayableDirector dir)
    {
        SceneManager.LoadScene(escenaSiguiente); // Cambia de escena
    }
}
