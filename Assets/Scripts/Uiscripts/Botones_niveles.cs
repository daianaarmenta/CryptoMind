using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Botones_niveles : MonoBehaviour
{
    private UIDocument menu; // Objeto de la UI en la escena
    private Button botonRegreso; // Bot�n para regresar a "MenuInicio"
    private Button nivel0;    // Bot�n para ir a "Nivel0"
    private Button tienda;

    void OnEnable()
    {
        menu = GetComponent<UIDocument>(); // Obtener el documento UI
        var root = menu.rootVisualElement; 

        // Buscar los botones en el UXML por su nombre
        botonRegreso = root.Q<Button>("botonRegresar");
        nivel0 = root.Q<Button>("n0");
        tienda = root.Q<Button>("botonTienda");

        // Registrar eventos de clic
        botonRegreso.RegisterCallback<ClickEvent, string>(CambiarEscena, "MenuInicio");
        nivel0.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel0");
        tienda.RegisterCallback<ClickEvent, string>(CambiarEscena, "Tienda");
    }

    private void CambiarEscena(ClickEvent evt, string escena)
    {
        SceneManager.LoadScene(escena);
    }
}
