using UnityEngine;
/*Autora: Daiana Andrea Armenta Maya A01751408
 * Fecha : 05/04/2025
 * Descripción: Clase que gestiona el comportamiento de las balas en el juego.
 * Controla la velocidad de la bala y su colisión con los enemigos.
 */
public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidadBala;
    private float daño;

    private void Start()
{
    daño = GameManagerBase.Instance.DañoBala;
}

    public void SetDaño(float nuevoDaño)
    {
        daño = nuevoDaño;
    }


    private void Update()
    {
        transform.Translate(Vector2.right * velocidadBala * Time.deltaTime); // Mueve la bala hacia la derecha
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo")) {
            other.GetComponent<Enemigo>().TomarDaño(daño);
            Destroy(gameObject); // Destruye la bala al colisionar con el enemigo
        }
    }
}



