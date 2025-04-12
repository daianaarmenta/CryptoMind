using UnityEngine;
using UnityEngine.SceneManagement;

public class BottonController : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa; // Referencia al menú de pausa
    [SerializeField] private GameObject menuPausa;// Referencia al botón de continuar
    
    public void Pausa()
    {
        Time.timeScale = 0f; // Pausa el juego
        botonPausa.SetActive(false); // Desactiva el botón de pausa
        menuPausa.SetActive(true); // Activa el menú de pausa
    }
    public void Reanudar(){
        Time.timeScale = 1f; // Reanuda el juego
        botonPausa.SetActive(true); // Desactiva el botón de pausa
        menuPausa.SetActive(false); // Activa el menú de pausa
    }
    public void Reiniciar(){
        Time.timeScale = 1f; // Reanuda el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }
    public void Salir(){
        Debug.Log("Saliendo del juego..."); // Mensaje de depuración
        Application.Quit(); // Cierra la aplicación

    }

    /*
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
    }*/
}

