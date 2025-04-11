using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioPantalla : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Cutscene 1";
    [SerializeField] private Animator fadeAnimator; // Referencia al Animator
    private bool activado = false; // Para evitar m�ltiples activaciones

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activado && other.CompareTag("Player"))
        {
            activado = true;
            fadeAnimator.SetTrigger("FadeOut"); // Activa la animaci�n
            Invoke("CambiarEscena", 1f); // Espera el tiempo de la animaci�n
        }
    }

    private void CambiarEscena()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
