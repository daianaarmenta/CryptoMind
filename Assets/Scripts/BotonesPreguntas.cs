using System;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UIElements;

/*
Codigo diseñado para los botones de las preguntas

Autor: Daiana Armenta

**/

public class BotonesPreguntas : MonoBehaviour
{
    private UIDocument preguntasBotones;
    private Button boton_si;
    private Button boton_no;

    void OnEnable()
    {    
        preguntasBotones = GetComponent<UIDocument>();
        var root = preguntasBotones.rootVisualElement;
        boton_si = root.Q<Button>("Si");
        boton_no = root.Q<Button>("No");


        //Callbacks
        boton_si.RegisterCallback<ClickEvent, String>(IniciarJuego, "Correcto");
        boton_no.RegisterCallback<ClickEvent, String>(IniciarJuego, "Incorrecto");
    }
    
    private void IniciarJuego(ClickEvent evt, String escena){
        SceneManager.LoadScene(escena);
    }
}

