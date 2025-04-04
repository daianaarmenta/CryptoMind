using UnityEngine;

public class MueveChabelito : MonoBehaviour
{
    [SerializeField] private float velocidadX;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private float velocidadEscalera;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");

        // Movimiento horizontal
        rb.linearVelocity = new Vector2(movHorizontal * velocidadX, rb.linearVelocity.y);

        // Movimiento en escaleras
        if (EstadoPersonaje.enEscalera)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, movVertical * velocidadEscalera);
            rb.gravityScale = 0;  // Desactivar gravedad
        }
        else
        {
            rb.gravityScale = 1;  // Restaurar gravedad
        }

        // Salto solo si está en el suelo
        if (Input.GetKeyDown(KeyCode.UpArrow) && EstadoPersonaje.enPiso)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Resetear salto acumulado
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }
}
