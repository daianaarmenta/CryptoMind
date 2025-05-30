using UnityEngine;
/* Autora: Daiana Andrea Armenta Maya
 * Descripción: Clase que gestiona el movimiento del personaje en el juego.
 */
public class MoverPersonaje : MonoBehaviour
{
    [SerializeField] private float velocidadX;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private float velocidadEscalera;

    public static bool estaAgachado { get; private set; }

    private Rigidbody2D rb;
    private bool mirandoDerecha = true; // Controla la dirección en la que está mirando el personaje

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");

        if (!estaAgachado)
        {
            // Movimiento horizontal
            rb.linearVelocity = new Vector2(movHorizontal * velocidadX, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Detener el movimiento horizontal al agacharse
        }

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
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && EstadoPersonaje.enPiso && !EstadoPersonaje.enEscalera)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Resetear salto acumulado
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            GetComponent<SonidosMovimiento>()?.ReproducirSalto();
        }

        // Cambio de dirección (giro del personaje)
        if (movHorizontal > 0 && !mirandoDerecha)
        {
            Girar(); // Si el personaje se mueve a la derecha y no está mirando a la derecha, giramos
        }
        else if (movHorizontal < 0 && mirandoDerecha)
        {
            Girar(); // Si el personaje se mueve a la izquierda y está mirando a la derecha, giramos
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            estaAgachado = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)  || Input.GetKeyUp(KeyCode.S))
        {
            estaAgachado = false;
        }
    }

    // Método para girar el personaje
    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }
}
