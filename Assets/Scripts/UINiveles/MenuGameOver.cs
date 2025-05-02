using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Autor: María Fernanda Pineda Pat
Controla el comportamiento del menu de Game Over, incluyendo su activación, 
manejo del reinicio del nivel y navegacion al menú niveles.
*/
public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver; // Referencia al menu game over de la UI.
    [SerializeField] private AudioClip sonidoGameOver; // Clip de sonido que se reproduce al morir el jugador. 
    [SerializeField] private AudioSource musicaFondo; // Fuente de música de fondo del juego. 

    private SaludPersonaje saludPersonaje;
    private AudioSource audioSource;

    // Se ejecutal al iniciar la escena. Busca al jugador y se suscribe el evento de muerte. 
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
            // Debug.LogWarning("No se encontró al jugador para activar el menú Game Over.");
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Método que se ejecuta cuando el jugador muere. Activa el menú de Game Over y reproduce el sonido correspondiente.
    private void ActiveMenu(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);
        musicaFondo.Stop();
        audioSource.PlayOneShot(sonidoGameOver, 1f); // Reproducir sonido de Game Over

        // Congelar movimiento del jugador sin errores
        Rigidbody2D rb = saludPersonaje.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false; // Detiene la simulación física sin necesidad de cambiar a Static
            
        }
    }
    
    // Método que se ejecuta al hacer clic en el botón de reiniciar. Reinicia el juego y recarga la escena actual.
        public void Reiniciar()
    {
        Time.timeScale = 1f;

        if (GameManagerBase.Instance != null)
        {
            GameManagerBase.Instance.ReiniciarVidas();          // Reinicia las vidas
            GameManagerBase.Instance.VolviendoDeTienda = false; // No viene de tienda
        }

        // Limpia posición guardada
        PlayerPrefs.DeleteKey("JugadorX");
        PlayerPrefs.DeleteKey("JugadorY");
        PlayerPrefs.DeleteKey("JugadorZ");
        PlayerPrefs.Save();

        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Método que se ejecuta al hacer clic en el botón de menú. Carga la escena del menú principal.
    public void MenuNiveles(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    // Método alternativo para mostrar el menú de Game Over desde otros scripts. 
    public void MostrarGameOver()
    {
        menuGameOver.SetActive(true);
        musicaFondo.Stop();
        audioSource.PlayOneShot(sonidoGameOver, 1f);

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
