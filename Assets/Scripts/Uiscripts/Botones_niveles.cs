using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Botones_niveles : MonoBehaviour
{
    private UIDocument menu; // Objeto de la UI en la escena
    private Button Nuevo; // Botón para regresar a "MenuInicio"
    private Button N1;    // Botón para ir a "Nivel1"

    void OnEnable()
    {
        menu = GetComponent<UIDocument>(); // Obtener el documento UI
        var root = menu.rootVisualElement; 

        // Buscar los botones en el UXML por su nombre
        Nuevo = root.Q<Button>("Nuevo");
        N1 = root.Q<Button>("N1");

        // Registrar eventos de clic
        Nuevo?.RegisterCallback<ClickEvent, string>(CambiarEscena, "MenuInicio");
        N1?.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel 1");
    }

    private void CambiarEscena(ClickEvent evt, string escena)
    {
        SceneManager.LoadScene(escena);
    }
}
