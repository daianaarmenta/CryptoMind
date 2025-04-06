using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidadBala;
    [SerializeField] private float daño;

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



