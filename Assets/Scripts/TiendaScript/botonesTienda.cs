using UnityEngine;
using UnityEngine.UIElements;

/*
Autor: María Fernanda Pineda Pat
Este codigo es para que funcione el boton de regresar que se encuentra en la parte superior izquierda*/

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
