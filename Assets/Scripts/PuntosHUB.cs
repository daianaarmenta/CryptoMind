using TMPro;
using UnityEngine;

public class PuntosHUB : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI puntos;

    void Start()
    {
        // Busca automáticamente el GameManager en la escena
        gameManager = FindFirstObjectByType<GameManager>();

        // Verifica que GameManager no sea null
        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado en la escena.");
        }

        // Verifica que el componente de texto está asignado
       // if (puntos == null)
        //{
          //  Debug.LogError("TextMeshProUGUI (puntos) no está asignado en el Inspector.");
        //}
    }

    void Update()
    {
        if (gameManager != null && puntos != null)
        {
            puntos.text = " " + gameManager.PuntosTotales.ToString();
        }
    }
}
