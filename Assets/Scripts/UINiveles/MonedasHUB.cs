using UnityEngine;
using TMPro;

/*
Autor: María Fernanda Pineda Pat
Este script actualiza dinamicamente el texto que muestra la cantidad de monedas del jugador en la interfaz de usuario,
mostrando el valor actual de monedas almacenado en el GameManager.
*/
public class MonedasHUB : MonoBehaviour
{
    public TextMeshProUGUI monedasTexto;

    // Se ejecuta al inicio. Verifica si el campo de texto fue asignado correctamente 
    void Start()
    {
        if (monedasTexto == null)
        {
            //Debug.LogError("No se asignó el campo monedasTexto en el Inspector.");
        }
    }

    // Se ejecuta una vez por frame. Actualiza el valor de monedas mostrado en pantalla.
    void Update()
    {
        if (monedasTexto != null)
        {
            monedasTexto.text = GameManagerBase.Instance.Monedas.ToString();
        }
    }
}
