using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class botonesTienda : MonoBehaviour
{
    public static string previousScene = ""; // Almacena la escena anterior al entrar a la tienda

    private UIDocument uiDoc;
    private Button botonRegresar;

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
            Debug.LogWarning("⚠️ No se encontró el botón 'botonRegresar' en el UI.");
        }
    }

    void Regresar()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            Debug.Log("🔁 Regresando a la escena anterior: " + previousScene);
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("⚠️ No se ha registrado una escena anterior. Cargando 'Menu_juego'.");
            SceneManager.LoadScene("Menu_juego"); // Cambia por el nombre exacto de tu menú si es diferente
        }
    }
}
