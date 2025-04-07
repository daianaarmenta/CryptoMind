using TMPro;
using UnityEngine;

public class PuntosHUB : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI puntos;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado en la escena.");
        }

        if (puntos == null)
        {
            Debug.LogError("TextMeshProUGUI (puntos) no está asignado en el Inspector.");
        }
    }

    void Update()
    {
        if (gameManager != null && puntos != null)
        {
            puntos.text = " " + gameManager.PuntosTotales.ToString();
        }
    }
}
