using UnityEngine;
/* Autora: Daiana Andrea Armenta Maya A01751408
 * Descripción: Clase que gestiona la animación del personaje en función de su estado y movimiento.
 * Cambia la animación del personaje según su velocidad, si está en el suelo o en una escalera.
 */
public class CambiaAnimacion : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spRenderer;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("velocidad", Mathf.Abs(rb.linearVelocity.x));
        //spRenderer.flipX = rb.linearVelocity.x < 0;
        animator.SetBool("enPiso", EstadoPersonaje.enPiso && !EstadoPersonaje.enEscalera);
        animator.SetBool("isClimbing", EstadoPersonaje.enEscalera);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("tienePistola");
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            animator.SetTrigger("recibeDaño"); // El personaje ha tocado el suelo
            Debug.Log("Recibe daño");
        }
    }

}

