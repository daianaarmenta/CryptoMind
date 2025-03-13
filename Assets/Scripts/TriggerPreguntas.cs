using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerPreguntas : MonoBehaviour
{
    [SerializeField] private string SceneName = "Preguntas";
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            SceneManager.LoadScene(SceneName);
        }
    }
}
