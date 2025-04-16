using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonesTiendaCanvas: MonoBehaviour
{
    //public static string previousScene = "";

    [SerializeField] private Button botonRegresar;

    void Start()
    {
        if (botonRegresar != null)
        {
            botonRegresar.onClick.AddListener(Regresar);
        }
        else
        {
            Debug.LogWarning("⚠️ No se asignó el botón 'botonRegresar'.");
        }
    }
    public void Regresar()
{
    string escenaAnterior = PlayerPrefs.GetString("EscenaAnterior", "Menu_juego");

    if (!string.IsNullOrEmpty(escenaAnterior))
    {
        SceneManager.LoadScene(escenaAnterior);
    }
    else
    {
        SceneManager.LoadScene("Menu_juego"); // fallback
    }
}

    /*public void Regresar()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            SceneManager.LoadScene("Menu_juego");
        }
    }*/
}
