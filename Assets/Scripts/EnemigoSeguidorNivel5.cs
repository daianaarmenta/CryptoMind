using UnityEngine;

public class EnemigoSeguidorNivel5 : MonoBehaviour
{
    [SerializeField] private Transform jugador;
    [SerializeField] private float velocidadNormal = 2f;
    [SerializeField] private float incrementoVelocidad = 1f; // Aumenta de 1 en 1
    [SerializeField] private float velocidadMaxima = 10f;    // Velocidad límite opcional
    [SerializeField] private GameObject efectoMuerte;



    private float velocidadActual;
    private bool activo = true;
    private Animator animator;

    private void Start()
    {
        velocidadActual = velocidadNormal;

        if (jugador == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) jugador = player.transform;
        }

        animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("caminar", true);
    }

    private void Update()
    {
        if (!activo || jugador == null) return;

        Vector3 direccion = (jugador.position - transform.position).normalized;
        transform.position += direccion * velocidadActual * Time.deltaTime;
    }

    public void Detener()
    {
        activo = false;
        if (animator != null)
            animator.SetBool("caminar", false);
            Debug.Log("⛔ Enemigo detenido");
    }

    public void Reanudar()
    {
        activo = true;
        if (animator != null)
            animator.SetBool("caminar", true);
    }

    public void Acelerar()
    {
        velocidadActual += incrementoVelocidad; // Aumenta la velocidad
        velocidadActual = Mathf.Min(velocidadActual, velocidadMaxima); // Opcional: no pasar del máximo

        Debug.Log("⚡ Enemigo acelerado a: " + velocidadActual);
    }

    public void ResetearVelocidad()
    {
        velocidadActual = velocidadNormal;
        Debug.Log("🟢 Velocidad reiniciada a: " + velocidadActual);
    }
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        Debug.Log("💥 El enemigo alcanzó al jugador. Game Over.");

        // Mostrar el menú de Game Over usando tu clase existente
        MenuGameOver gameOver = FindFirstObjectByType<MenuGameOver>();

        if (gameOver != null)
        {
            gameOver.MostrarGameOver(); // Usa tu método personalizado
        }
        else
        {
            Debug.LogWarning("⚠️ No se encontró el objeto MenuGameOver en la escena.");
        }

        // (Opcional) Pausar el juego completamente
        Time.timeScale = 0f;
    }
}

public void Morir()
{
    activo = false;
    Vector3 centroPantalla = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    centroPantalla.z = transform.position.z; // Mantén la profundidad
    transform.position = centroPantalla;

    //if (animator != null)
      //  animator.SetBool("caminar", false); // ⛔ Deten animación de caminar si es necesario

    if (efectoMuerte != null)
    {
        Instantiate(efectoMuerte, transform.position, Quaternion.identity); // 💥 Efecto visual
    }

    Debug.Log("💥 Enemigo destruido con efecto");

    Destroy(gameObject); // ✅ Destruye el enemigo
}

}
