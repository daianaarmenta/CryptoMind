using UnityEngine;
using UnityEngine.UIElements;

public class botonesTienda: MonoBehaviour
{
    UIDocument boton;
    Button botonRegresar
;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        boton = GetComponent<UIDocument>();
        var root = boton.rootVisualElement; 
        botonRegresar = root.Q<Button>("botonRegresar");
        botonRegresar.RegisterCallback<ClickEvent>(Regresar);
    }
    void Regresar(ClickEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu_juego");
    }
}
