/*
 * Autor: Ra�l Maldonado Pineda - A01276808
 * Script: ScriptCreditos.cs
 * Descripci�n: Este script maneja el desplazamiento de los cr�ditos en la pantalla, desplaz�ndolos hacia arriba
 *              a una velocidad definida. Al alcanzar una posici�n determinada, carga autom�ticamente la escena
 *              del men� principal del juego.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptCreditos : MonoBehaviour
{
    // Velocidad de desplazamiento de los cr�ditos hacia arriba
    public float scrollSpeed = 20f;

    // Posici�n final en el eje Y cuando los cr�ditos han llegado al final
    public float limiteFinalY = 1000f; // Ajusta este valor seg�n el tama�o de los cr�ditos

    // Nombre de la escena del men� principal
    public string nombreEscenaMenu = "MenuJuego";

    private RectTransform rectTransform; // Referencia al RectTransform del objeto que contiene los cr�ditos

    // Al inicio, obtenemos el RectTransform del objeto para manipular su posici�n
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // En cada frame, movemos los cr�ditos hacia arriba
    private void Update()
    {
        // Desplazamos los cr�ditos hacia arriba (en el eje Y)
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        // Si los cr�ditos han alcanzado el l�mite final, cargamos la escena del men�
        if (rectTransform.anchoredPosition.y >= limiteFinalY)
        {
            SceneManager.LoadScene(nombreEscenaMenu);
        }
    }
}
