using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

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

            // ✅ Sincroniza con el GameManager al iniciar
            vidasMaximas = GameManager.Instance.MaxVidas;
            vidas = GameManager.Instance.VidasGuardadas;

            //DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void PerderVida()
    {
        if (vidas > 0)
        {
            vidas--;

            // ✅ Sincroniza con GameManager
            GameManager.Instance.VidasGuardadas = vidas;

            Debug.Log("Vida perdida. Vidas restantes: " + vidas);

            if (VidasHUD.instance != null)
            {
                VidasHUD.instance.ActualizarVidas();
            }

            if (vidas <= 0)
            {
                MuerteJugador?.Invoke(this, EventArgs.Empty);
                //SceneManager.LoadScene("Game Over");
            }
        }
    }

    /*public void RegresarAEscenaPrincipal(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }*/
}
