using System;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UIElements;

/*
Codigo diseñado para pasar de la pagina de inicio a la pagina de registo o se login, dependiendo de lo que el usario decida

Autor: Emiliano PLata Cardona

**/

public class BotonesInicio : MonoBehaviour
{
    private UIDocument menu;
    private Button login;
    private Button registro;

    void OnEnable()
    {    
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;
        login = root.Q<Button>("boton_login");
        registro = root.Q<Button>("boton_registrarse");


        //Callbacks
        login.RegisterCallback<ClickEvent, String>(IniciarJuego, "Login");
        registro.RegisterCallback<ClickEvent, String>(IniciarJuego, "Registro");
    }
    
    private void IniciarJuego(ClickEvent evt, String escena){
        SceneManager.LoadScene(escena);
    }
}
