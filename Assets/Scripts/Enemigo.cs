using UnityEngine;

/*Autora: Daiana Andrea Armenta Maya
    * Fecha : 05/04/2025
    * Descripción: Clase que gestiona el comportamiento de los enemigos en el juego.
    * Controla la vida del enemigo, el daño recibido y la muerte del enemigo.
    */
public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private GameObject efectoMuerte;

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0) {
            Muerte();
        }
        Debug.Log("Vida restante: " + vida); // Muestra la vida restante en la consola
    }

    private void Muerte() {
        Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        Destroy(gameObject); // Destruye el objeto enemigo
    }

        void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SaludPersonaje.instance.PerderVida(); // Llama al método para perder vida del jugador
        }
    }
}
