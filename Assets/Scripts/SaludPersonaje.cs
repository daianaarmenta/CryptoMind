using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class SaludPersonaje : MonoBehaviour
{
    public int vidas = 5;
    public int vidasMaximas = 5;
    //public float tiempoRegeneracion = 5f; // Tiempo en segundos para regenerar una vida

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
            //DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
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
            Debug.Log("Vida perdida. Vidas restantes: " + vidas);

            if (VidasHUD.instance != null)
            {
                VidasHUD.instance.ActualizarVidas();
            }

            if (vidas <= 0)
            {
                MuerteJugador?.Invoke(this, EventArgs.Empty); // Llama al evento de muerte  
                
                //SceneManager.LoadScene("Game Over"); // Cambia de escena cuando las vidas sean 0
            }
        }
    }
    

    /*public void RegresarAEscenaPrincipal(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }*/
}
