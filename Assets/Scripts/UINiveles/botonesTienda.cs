using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/*
Autor: María Fernanda Pineda Pat
Controla el botón de rerreso en la tienda utilizando UI Toolkit.
Permite volver a la escena anterior almacenada en 'previousScene' o al menú principal si no hay escena anterior.
*/
public class botonesTienda : MonoBehaviour
{
    public static string previousScene = ""; // Almacena la escena anterior al entrar a la tienda

    private UIDocument uiDoc;
    private Button botonRegresar;

    // Se ejecuta automáticamente cuando el GameObject se activa. Se conecta al botón de la interfaz y asigna el evento de clic.
    void OnEnable()
    {
        uiDoc = GetComponent<UIDocument>();
        var root = uiDoc.rootVisualElement;

        botonRegresar = root.Q<Button>("botonRegresar");

        if (botonRegresar != null)
        {
            botonRegresar.clicked += Regresar;
        }
        else
        {
            //Debug.LogWarning(" No se encontró el botón 'botonRegresar' en el UI.");
        }
    }

    // Carga la escena anterior si está definida. Si no, carga por defecto la escena del menú principal
    void Regresar()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            //Debug.Log(" Regresando a la escena anterior: " + previousScene);
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            //Debug.LogWarning(" No se ha registrado una escena anterior. Cargando 'Menu_juego'.");
            SceneManager.LoadScene("Menu_juego"); // Cambia por el nombre exacto de tu menú si es diferente
        }
    }
}
