using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class botonesTienda : MonoBehaviour
{
    public static string previousScene = "";

    UIDocument boton;
    Button botonRegresar;

    void OnEnable()
    {
        boton = GetComponent<UIDocument>();
        var root = boton.rootVisualElement;

        botonRegresar = root.Q<Button>("botonRegresar");
        if (botonRegresar != null)
        {
            botonRegresar.RegisterCallback<ClickEvent>(Regresar);
        }
        else
        {
            Debug.LogWarning("No se encontró el botón 'botonRegresar' en la UI.");
        }
    }

    void Regresar(ClickEvent evt)
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            Debug.Log("Regresando a: " + previousScene);
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("No se ha registrado una escena anterior. Cargando 'Menu_juego'.");
            SceneManager.LoadScene("Menu_juego");
        }
    }
}