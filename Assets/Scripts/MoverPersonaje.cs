using UnityEngine;

public class MueveChabelito : MonoBehaviour
{
    [SerializeField] private float velocidadX;
    [SerializeField] private float fuerzaSalto;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movHorizontal = Input.GetAxis("Horizontal");

        // Movimiento horizontal
        rb.linearVelocity = new Vector2(movHorizontal * velocidadX, rb.linearVelocity.y);

        // Salto solo si está en el suelo
        if (Input.GetKeyDown(KeyCode.UpArrow) && EstadoPersonaje.enPiso)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // resetear salto acumulado
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }
}