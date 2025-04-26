using UnityEngine;

/* Autora: Daiana Andrea Armenta Maya
 * Descripción: Clase que gestiona el estado del personaje en el juego.
 * Controla si el personaje está en el suelo o en una escalera mediante colisiones con objetos correspondientes.
 */
public class EstadoPersonaje : MonoBehaviour
{
    public static bool enPiso { get; private set; }
    public static bool enEscalera { get; set; }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            enPiso = true;
            //Debug.Log("Entró al suelo");
        }

        if (collision.CompareTag("Escaleras"))
        {
            enEscalera = true;
            //Debug.Log("Entró a escalera");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            enPiso = false;
            //Debug.Log("Salió del suelo");
        }

        if (collision.CompareTag("Escaleras"))
        {
            enEscalera = false;
            Debug.Log("Salió de escalera");
        }
    }
}



