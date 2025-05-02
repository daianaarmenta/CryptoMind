using UnityEngine;
/*Autora: Daiana Andrea Armenta Maya
 * Descripción: Clase que controla el comportamiento de los enemigos en el juego.
 * Permite gestionar la vida, efectos de muerte y colisiones con el jugador.
 */
public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private GameObject efectoMuerte;
    [SerializeField] private float cantidadPuntos;
    [SerializeField] private AudioClip sonidoMuerte;

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
        GameManagerBase.Instance.SumarPuntaje((int)cantidadPuntos); // Sumar puntos al jugador

        if(sonidoMuerte != null)
        {
            AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position,3f);
        }


        if (efectoMuerte != null)
        {
            Instantiate(efectoMuerte, transform.position, Quaternion.identity); // Instancia el efecto de muerte en la posición del enemigo
        
        }

        Destroy(gameObject); // Destruye el objeto enemigo
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SaludPersonaje.instance?.PerderVida();
        }
    }
}
