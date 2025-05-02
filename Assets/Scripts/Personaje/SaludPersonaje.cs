using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

/*
Autor: María Fernanda Pineda Pat
Gestiona la salud del personaje (número de vidas) y el evento de muerte del jugador.
Se comporta de forma diferente en el nivel 5 (escena "Nivel5") donde no se utilizan vidas.
*/
    public class SaludPersonaje : MonoBehaviour
    {
        public int vidas = 5;
        public int vidasMaximas = 5;
        public static SaludPersonaje instance;
        internal int numeroMonedas;
        public event EventHandler MuerteJugador;

    // Se ejecuta al crear la instancia. Inicializa vidas y configura el comportamiento especial para Nivel 5.
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            string escena = SceneManager.GetActiveScene().name;

            if (escena == "Nivel5" || escena.Contains("5"))
            {
                //Debug.Log("Nivel 5: Desactivando sistema de vidas.");
                vidas = 0; // o cualquier valor, ya no importa
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Se ejecuta al comenzar el juego. Asegura que el HUD muestre las vidas actuales.
    private void Start()
    {
        VidasHUD.instance?.ActualizarVidas(); // HUD siempre actualizado
    }

    // Lógica que se ejecuta cuando el jugador pierde una vida. Si llega a 0, se dispara el evento de muerte. En Nivel 5, se muestra Game Over directo.
    public void PerderVida()
    {
        string escena = SceneManager.GetActiveScene().name;

        if (escena == "Nivel5" || escena.Contains("5"))
        {
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

            GameManagerBase.Instance.VidasGuardadas = vidas;

            //Debug.Log("Vida perdida. Vidas restantes: " + vidas);

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
