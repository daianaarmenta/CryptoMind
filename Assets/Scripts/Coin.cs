using UnityEngine;

public class Coin : MonoBehaviour
{
    public int valor = 1;
    private GameManager gameManager;

    void Start()
    {
        // Encuentra el GameManager automáticamente en la escena usando la nueva función recomendada por Unity
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado en la escena. Asegúrate de que existe en la jerarquía.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.SumarPuntos(valor);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("No se pudo sumar puntos porque gameManager es null.");
            }
        }
    }
}
    
