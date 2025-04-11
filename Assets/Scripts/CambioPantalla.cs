using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioPantalla : MonoBehaviour
{
    [SerializeField] public string sceneToLoad = "Cutscene 1";

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
