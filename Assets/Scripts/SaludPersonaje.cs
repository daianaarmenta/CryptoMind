using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class SaludPersonaje : MonoBehaviour
{
    public int vidas = 5;
    public int vidasMaximas = 5;
    public static SaludPersonaje instance;
    internal int numeroMonedas;
    public event EventHandler MuerteJugador;

void Awake()
{
    if (instance == null)
    {
        instance = this;

        string escena = SceneManager.GetActiveScene().name;

        if (escena == "Nivel5" || escena.Contains("5"))
        {
            Debug.Log("🧪 Nivel 5: Desactivando sistema de vidas.");
            vidas = 0; // o cualquier valor, ya no importa
        }
        else
        {
            vidasMaximas = GameManager.Instance.MaxVidas;
            vidas = GameManager.Instance.VidasGuardadas;
        }
    }
    else
    {
        Destroy(gameObject);
    }
}


    private void Start()
    {
        VidasHUD.instance?.ActualizarVidas(); // HUD siempre actualizado
    }

    public void PerderVida()
    {
        string escena = SceneManager.GetActiveScene().name;

    if (escena == "Nivel5" || escena.Contains("5"))
    {
        // 💀 Game Over directo sin usar vidas
        MenuGameOver gameOver = FindFirstObjectByType<MenuGameOver>();
        if (gameOver != null)
        {
            gameOver.MostrarGameOver();
        }
        return;
    }
        if (vidas > 0)
        {
            vidas--;

            // 🔄 Guarda cambio en GameManager
            GameManager.Instance.VidasGuardadas = vidas;

            Debug.Log("Vida perdida. Vidas restantes: " + vidas);

            VidasHUD.instance?.ActualizarVidas();

            if (vidas <= 0)
            {
                MuerteJugador?.Invoke(this, EventArgs.Empty);

                PlayerPrefs.DeleteKey("JugadorX");
                PlayerPrefs.DeleteKey("JugadorY");
                PlayerPrefs.DeleteKey("JugadorZ");
                PlayerPrefs.Save();
            }
        }
    }
}
