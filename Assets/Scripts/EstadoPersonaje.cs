using UnityEngine;

/*Autora: Daiana Andrea Armenta Maya 
    
    * Descripción: Clase que gestiona el estado del personaje en el juego.
    * Controla si el personaje está en el suelo o en una escalera mediante colisiones con los objetos correspondientes.
    */
public class EstadoPersonaje : MonoBehaviour
{
    public static bool enPiso { get; private set; }
    public static bool enEscalera { get; private set; }

    void Start()
    {
        enPiso = false;
        enEscalera = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo") && !enEscalera)
        {
            enPiso = true;
        }
        else if (collision.CompareTag("Escaleras"))
        {
            enEscalera = true;
            enPiso = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            enPiso = false;
        }
        else if (collision.CompareTag("Escaleras"))
        {
            enEscalera = false;
        }
    }
}

