using UnityEngine;
using UnityEngine.SceneManagement;

public class BottonController : MonoBehaviour
{
    [SerializeField] private string siguienteEscena = "Menu_juego";

    public void NextScene()
    {
        // Solo guardar la escena actual si vamos a la tienda
        if (siguienteEscena == "Tienda")
        {
            botonesTienda.previousScene = SceneManager.GetActiveScene().name;
            Debug.Log("Escena actual guardada como 'anterior': " + botonesTienda.previousScene);
        }

        SceneManager.LoadScene(siguienteEscena);
    }
}

