using UnityEngine;
/*
Modifica los parametros del animator del personaje 
Autor: Fernanda Pineda
*/
public class CambiaAnimacion : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spRenderer;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        spRenderer= GetComponent<SpriteRenderer>();
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Modificar el parámetro del animator "Velocidad"
        animator.SetFloat("velocidad", Mathf.Abs(rb.linearVelocityX));
        spRenderer.flipX = rb.linearVelocityX < 0;  
        animator.SetBool("enPiso", EstadoPersonaje.enPiso);
    }
}
