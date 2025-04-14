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
    public void Reiniciar()
    {
        Time.timeScale = 1f; // Reanuda el tiempo si estaba pausado

        // ✅ Reiniciar vidas antes de recargar la escena
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarVidas();
            Debug.Log("🔁 Vidas reiniciadas.");
        }
        else
        {
            Debug.LogWarning("⚠️ GameManager no encontrado al reiniciar.");
        }

        // 🔄 Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Tienda()
    {
        botonesTienda.previousScene = SceneManager.GetActiveScene().name; // 🟢 Guardar escena actual
        SceneManager.LoadScene("Tienda"); // 🔁 Cargar tienda
    }


    public void Salir(){
        SceneManager.LoadScene(1);     
    }
}

