using UnityEngine;
using UnityEngine.SceneManagement;

public class BottonController : MonoBehaviour
{
    [SerializeField] private string siguenteEscena = "Menu_juego";
    public void NextScene(){
        SceneManager.LoadScene(siguenteEscena);
    }
}
