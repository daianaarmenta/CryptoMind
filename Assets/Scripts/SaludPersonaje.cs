using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaludPersonaje : MonoBehaviour
{
    public int vidas = 3;
    public int vidasMaximas = 3;
    public float tiempoRegeneracion = 5f; // Tiempo en segundos para regenerar una vida

    public static SaludPersonaje instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
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
                SceneManager.LoadScene("Game Over"); // Cambia de escena cuando las vidas sean 0
            }
        }
    }

    public void RegresarAEscenaPrincipal(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
