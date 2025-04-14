using UnityEngine;

/*
Autor: Fernanda Pineda 
Este código es para gestionar la recolección de monedas en el juego.
*/
public class Coin : MonoBehaviour
{
    [Header("Valor de la moneda")]
    public int valor = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GetComponent<SpriteRenderer>().enabled)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SumarMonedas(valor); 
            }

            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 0.9f);
        }
    }
}
