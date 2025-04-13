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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuNiveles(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
