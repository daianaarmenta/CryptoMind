using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private GameObject efectoMuerte;
    [SerializeField] private float cantidadPuntos;

    public void TomarDaño(float daño)
    {
        vida -= daño;
        Debug.Log("Vida restante: " + vida);

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        GameManager.Instance.SumarPuntaje((int)cantidadPuntos);


        if (efectoMuerte != null)
        {
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SaludPersonaje.instance?.PerderVida();
        }
    }
}
