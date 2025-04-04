using UnityEngine;

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
        spRenderer.flipX = rb.linearVelocity.x < 0;
        animator.SetBool("enPiso", EstadoPersonaje.enPiso && !EstadoPersonaje.enEscalera);
        animator.SetBool("isClimbing", EstadoPersonaje.enEscalera);
    }
}

