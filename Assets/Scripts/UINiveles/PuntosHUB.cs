using TMPro;
using UnityEngine;

/*
Autor: María Fernanda Pineda Pat
Este script muestra el puntaje del jugador en la interfaz de usuario, 
actualizandolo constantemente a partir del GameManager.
*/
public class PuntosHUB : MonoBehaviour
{
    private GameManagerBase gameManager;
    public TextMeshProUGUI puntos;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManagerBase>();

        if (gameManager == null)
        {
            //Debug.LogError("GameManager no encontrado en la escena.");
        }

        if (puntos == null)
        {
            //Debug.LogError("TextMeshProUGUI (puntos) no está asignado en el Inspector.");
        }
    }

    void Update()
    {
        if (gameManager != null && puntos != null)
        {
            puntos.text = " " + gameManager.Puntaje.ToString(); 
        }
    }
}
