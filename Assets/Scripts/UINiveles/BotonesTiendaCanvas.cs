using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
Autor: María Fernanda Pineda Pat
Controla el botón de regreso en la tienda utilizando el sistema de UI con Canvas.
Recupera la escena anterior desde PlayerPrefs para regresar al menú o al nivel anterior.
*/
public class BotonesTiendaCanvas: MonoBehaviour
{
    [SerializeField] private Button botonRegresar;

    // Al iniciar la escena, asigna la función Regresar al botón si está correctamente referenciado.
    void Start()
    {
        if (botonRegresar != null)
        {
            botonRegresar.onClick.AddListener(Regresar);
        }
        else
        {
            // Debug.LogWarning(" No se asignó el botón 'botonRegresar'.");
        }
    }

    // Carga la escena anterior guardada en PlayerPrefs. Si no hay ninguna guardada, se carga "Menu_juego" por defecto. 
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
}
