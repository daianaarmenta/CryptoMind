using UnityEngine;

/* Autora: Daiana Andrea Armenta Maya
 * Descripción: Clase que gestiona el estado del personaje en el juego.
 * Controla si el personaje está en el suelo o en una escalera mediante colisiones con objetos correspondientes.
 */
public class EstadoPersonaje : MonoBehaviour
{
    private static int contadorSuelo = 0;
    private static int contadorEscalera = 0;

    public static bool enPiso => contadorSuelo > 0;
    public static bool enEscalera => contadorEscalera > 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            contadorSuelo++;
            //Debug.Log("Entró al suelo. contadorSuelo = " + contadorSuelo);
        }

        if (collision.CompareTag("Escaleras"))
        {
            contadorEscalera++;
            //Debug.Log("Entró a escalera. contadorEscalera = " + contadorEscalera);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            contadorSuelo = Mathf.Max(0, contadorSuelo - 1);
            //Debug.Log("Salió del suelo. contadorSuelo = " + contadorSuelo);
        }

        if (collision.CompareTag("Escaleras"))
        {
            contadorEscalera = Mathf.Max(0, contadorEscalera - 1);
            //Debug.Log("Salió de escalera. contadorEscalera = " + contadorEscalera);
        }
    }
}


