using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    private SaludPersonaje saludPersonaje;

    void Start()
    {
        // Buscar al jugador y suscribirse al evento de muerte
        saludPersonaje = GameObject.FindGameObjectWithTag("Player")?.GetComponent<SaludPersonaje>();

        if (saludPersonaje != null)
        {
            saludPersonaje.MuerteJugador += ActiveMenu;
        }
        else
        {
            Debug.LogWarning("⚠️ No se encontró al jugador para activar el menú Game Over.");
        }
    }

    private void ActiveMenu(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);

        // Congelar movimiento del jugador sin errores
        Rigidbody2D rb = saludPersonaje.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false; // Detiene la simulación física sin necesidad de cambiar a Static
            
        }
    }
    public void Reiniciar()
{
    if (GameManager.Instance != null)
    {
        GameManager.Instance.ReiniciarVidas(); // 🔁 Restaurar las 5 vidas
        GameManager.Instance.VolviendoDeTienda = false; // Por si venías de tienda
    }

    // 🧹 (Opcional) Limpiar posición guardada por si venías de tienda
    PlayerPrefs.DeleteKey("JugadorX");
    PlayerPrefs.DeleteKey("JugadorY");
    PlayerPrefs.DeleteKey("JugadorZ");
    PlayerPrefs.Save();

    // 🔄 Recargar la escena actual
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}

    public void MenuNiveles(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
    public void MostrarGameOver()
{
    menuGameOver.SetActive(true);

    if (saludPersonaje == null)
        saludPersonaje = GameObject.FindGameObjectWithTag("Player")?.GetComponent<SaludPersonaje>();

    if (saludPersonaje != null)
    {
        Rigidbody2D rb = saludPersonaje.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }
    }
}

}
