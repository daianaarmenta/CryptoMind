using UnityEngine;

public class MueveChabelito : MonoBehaviour
{
    // Velocidades
    public float velocidadX;

    [SerializeField] // Permite al editor de Unity acceder a la variable
    private float velocidadY;

    // Necesito que el personaje se mueva
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Inicializar rb
        rb = GetComponent<Rigidbody2D>();  
    }

    // FixedUpdate is called 60 fps
    void FixedUpdate()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");

        if (movVertical >0)
        {
            rb.linearVelocity =  new Vector2(rb.linearVelocityX, movVertical * velocidadY);
        } else{
            rb.linearVelocity =  new Vector2(movHorizontal * velocidadX, rb.linearVelocityY);
        }    

    }

}