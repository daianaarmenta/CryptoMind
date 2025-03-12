using System;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UIElements;

/*
Codigo diseñado para el regreso de la pregunta a la escena de juego

Autor: Daiana Armenta

**/

public class BotonesRegresoPregunta : MonoBehaviour
{
    private UIDocument botonRegresoPregunta;
    private Button ok;

    void OnEnable()
    {    
        botonRegresoPregunta = GetComponent<UIDocument>();
        var root = botonRegresoPregunta.rootVisualElement;
        ok = root.Q<Button>("Button");


        //Callbacks
        ok.RegisterCallback<ClickEvent, String>(IniciarJuego, "Nivel 1");
    }
    
    private void IniciarJuego(ClickEvent evt, String escena){
        SceneManager.LoadScene(escena);
    }
}
