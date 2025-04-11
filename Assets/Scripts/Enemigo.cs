using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/*Autora: Daiana Andrea Armenta Maya
    * Fecha : 05/04/2025
    * Descripción: Clase que gestiona el comportamiento de los enemigos en el juego.
    * Controla la vida del enemigo, el daño recibido y la muerte del enemigo.
    */
public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private GameObject efectoMuerte;
    [SerializeField] private float cantidadPuntos; // Daño que el enemigo inflige al jugador
    [SerializeField] private PuntajeEnemigo puntaje; // Daño que el enemigo inflige al jugador

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0) {
            Muerte();
        }
        Debug.Log("Vida restante: " + vida); // Muestra la vida restante en la consola
    }

    private void Muerte() {
        puntaje.SumarPuntos(cantidadPuntos); // Suma puntos al puntaje del jugador
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
