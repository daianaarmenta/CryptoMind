using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Valor de la moneda")]
    public int valor = 10; // Puedes editar esto en el Inspector para monedas con distinto valor

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GetComponent<SpriteRenderer>().enabled)
        {
            // Sumar puntos al GameManager (se refleja en el HUD)
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SumarPuntos(valor);
            }

            // Ocultar y destruir moneda
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 0.9f);
        }
    }
}
