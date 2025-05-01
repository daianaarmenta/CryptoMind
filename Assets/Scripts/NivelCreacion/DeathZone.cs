using UnityEngine;

/*
Autor: María Fernanda Pineda Pat
Este script representa una zona de muerte que al ser tocada por el jugaror, se va directamente al menú de Game Over.
*/
public class DeathZone : MonoBehaviour
{
    private MenuGameOver menuGameOver;

    // Busca el objeto MenuGameOver al iniciar la escena.
    void Start()
    {
        menuGameOver = FindFirstObjectByType<MenuGameOver>();
        if (menuGameOver == null)
        {
            //Debug.LogWarning(" No se encontró MenuGameOver en la escena.");
        }
    }

    //  Detecta cuando el jugador entra en la zona de muerte (colisión tipo trigger).
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && menuGameOver != null)
        {
            //Debug.Log(" Jugador cayó en la DeathZone");
            menuGameOver.MostrarGameOver(); 
        }
    }
}
