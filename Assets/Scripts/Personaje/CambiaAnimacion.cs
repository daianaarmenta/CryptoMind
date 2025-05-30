using System;
using UnityEngine;
/* Autora: Daiana Andrea Armenta Maya A01751408
 * Descripción: Clase que gestiona la animación del personaje en función de su estado y movimiento.
 * Cambia la animación del personaje según su velocidad, si está en el suelo o en una escalera.
 */
public class CambiaAnimacion : MonoBehaviour
{
    private Rigidbody2D rb;
    //private SpriteRenderer spRenderer;
    private Animator animator;
    [SerializeField] private AudioClip sonidoDaño;
    private AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //spRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        animator.SetFloat("velocidadHorizontal", Mathf.Abs(rb.linearVelocity.x));
        if (Math.Abs(rb.linearVelocity.y) > Mathf.Epsilon)
        {
            animator.SetFloat("velocidadVertical", Mathf.Abs(rb.linearVelocity.y));
        }
        else
        {
            animator.SetFloat("velocidadVertical", 0);
        }

        //spRenderer.flipX = rb.linearVelocity.x < 0;
        animator.SetBool("enPiso", EstadoPersonaje.enPiso && !EstadoPersonaje.enEscalera);
        animator.SetBool("isClimbing", EstadoPersonaje.enEscalera);
        animator.SetBool("estaAgachado", MoverPersonaje.estaAgachado);

        if (Input.GetKeyDown(KeyCode.Space) && !DialogueController.isDialogueOpen)
        {
            animator.SetTrigger("tienePistola");
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            animator.SetTrigger("recibeDaño"); // El personaje ha tocado el suelo
            audioSource.PlayOneShot(sonidoDaño, 1f);
            Debug.Log("Recibe daño");
        }

    }


}

