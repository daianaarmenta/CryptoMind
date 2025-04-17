using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class SaludPersonaje : MonoBehaviour
{
    public int vidas = 5;
    public int vidasMaximas = 5;

    private bool isInstanceAlive = false;
    public static SaludPersonaje instance;
    internal int numeroMonedas;
    public event EventHandler MuerteJugador;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            isInstanceAlive = true;

            vidasMaximas = GameManager.Instance.MaxVidas;
            vidas = GameManager.Instance.VidasGuardadas; // Solo una vez
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
