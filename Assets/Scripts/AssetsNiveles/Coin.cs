using UnityEngine;

/*
Autor: Fernanda Pineda 
Este código es para gestionar la recolección de monedas por parte del juego.
Al tocar una moneda, suma su valor al GameManager, reproduce un sonido y se destruye.
*/
public class Coin : MonoBehaviour
{
    [Header("Valor de la moneda")]
    public int valor = 10;
    [Header("Sonido de recolección")]
    public AudioClip sonidoMoneda; // Sonido de la moneda

    // Detecta colisiones con el jugador. Si colisiona y la moneda aún está visible, la recoge.
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GetComponent<SpriteRenderer>().enabled)
        {
            if (GameManagerBase.Instance != null)
            {
                GameManagerBase.Instance.SumarMonedas(valor); 
            }

            AudioSource.PlayClipAtPoint(sonidoMoneda, transform.position); // Reproducir sonido de la moneda

            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 0.9f);
        }
    }
}
