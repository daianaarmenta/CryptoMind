/*
 * Autor: Raúl Maldonado Pineda - A01276808
 * Script: ScriptCreditos.cs
 * Descripción: Este script maneja el desplazamiento de los créditos en la pantalla, desplazándolos hacia arriba
 *              a una velocidad definida. Al alcanzar una posición determinada, carga automáticamente la escena
 *              del menú principal del juego.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptCreditos : MonoBehaviour
{
    // Velocidad de desplazamiento de los créditos hacia arriba
    public float scrollSpeed = 20f;

    // Posición final en el eje Y cuando los créditos han llegado al final
    public float limiteFinalY = 1000f; // Ajusta este valor según el tamaño de los créditos

    // Nombre de la escena del menú principal
    public string nombreEscenaMenu = "MenuJuego";

    private RectTransform rectTransform; // Referencia al RectTransform del objeto que contiene los créditos

    // Al inicio, obtenemos el RectTransform del objeto para manipular su posición
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // En cada frame, movemos los créditos hacia arriba
    private void Update()
    {
        // Desplazamos los créditos hacia arriba (en el eje Y)
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        // Si los créditos han alcanzado el límite final, cargamos la escena del menú
        if (rectTransform.anchoredPosition.y >= limiteFinalY)
        {
            SceneManager.LoadScene(nombreEscenaMenu);
        }
    }
}
