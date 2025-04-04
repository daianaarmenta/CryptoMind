using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Botones_niveles : MonoBehaviour
{
    private UIDocument menu; // Objeto de la UI en la escena
    private Button botonRegreso; // Bot�n para regresar a "MenuInicio"
    private Button nivel1;    // Bot�n para ir a "Nivel1"

    void OnEnable()
    {
        menu = GetComponent<UIDocument>(); // Obtener el documento UI
        var root = menu.rootVisualElement; 

        // Buscar los botones en el UXML por su nombre
        botonRegreso = root.Q<Button>("Nuevo");
        nivel1 = root.Q<Button>("N1");

        // Registrar eventos de clic
        botonRegreso.RegisterCallback<ClickEvent, string>(CambiarEscena, "MenuInicio");
        nivel1.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel 1");
    }

    private void CambiarEscena(ClickEvent evt, string escena)
    {
        SceneManager.LoadScene(escena);
    }
}
