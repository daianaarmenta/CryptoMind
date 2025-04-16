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
    Time.timeScale = 1f;

    if (GameManager.Instance != null)
    {
        GameManager.Instance.ReiniciarVidas();           // ✅ Reinicia vidas en PlayerPrefs
        GameManager.Instance.VolviendoDeTienda = false;  // ⛔ no viene de tienda
    }

    // 🧹 Limpia la posición guardada
    PlayerPrefs.DeleteKey("JugadorX");
    PlayerPrefs.DeleteKey("JugadorY");
    PlayerPrefs.DeleteKey("JugadorZ");
    PlayerPrefs.Save();

    // ✅ Reiniciar la escena solo después de todo
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
public void Tienda()
{
    string escenaActual = SceneManager.GetActiveScene().name;

    // Guarda el nombre de la escena actual SIEMPRE
    PlayerPrefs.SetString("EscenaAnterior", escenaActual);
    PlayerPrefs.Save();

    // Guarda posición solo si NO vienes del menú
    if (escenaActual != "Menu_juego")
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

        if (jugador != null)
        {
            Vector3 pos = jugador.transform.position;
            PlayerPrefs.SetFloat("JugadorX", pos.x);
            PlayerPrefs.SetFloat("JugadorY", pos.y);
            PlayerPrefs.SetFloat("JugadorZ", pos.z);
            GameManager.Instance.VolviendoDeTienda = true;
        }
    }
    else
    {
        GameManager.Instance.VolviendoDeTienda = false;
    }

    SceneManager.LoadScene("Tienda");
}



    public void Salir(){
        SceneManager.LoadScene(1);     
    }
    
}

