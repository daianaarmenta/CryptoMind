using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    [SerializeField] private AudioClip sonidoGameOver;
    [SerializeField] private AudioSource musicaFondo;

    private SaludPersonaje saludPersonaje;
    private AudioSource audioSource;


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
        audioSource = GetComponent<AudioSource>();
    }

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
    
        public void Reiniciar()
    {
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarVidas();          // Reinicia las vidas
            GameManager.Instance.VolviendoDeTienda = false; // No viene de tienda
        }

        // Limpia posición guardada
        PlayerPrefs.DeleteKey("JugadorX");
        PlayerPrefs.DeleteKey("JugadorY");
        PlayerPrefs.DeleteKey("JugadorZ");
        PlayerPrefs.Save();

        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuNiveles(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
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
