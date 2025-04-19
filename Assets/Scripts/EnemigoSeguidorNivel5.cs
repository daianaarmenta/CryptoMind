using UnityEngine.Playables;
using UnityEngine;
using System.Collections;


public class EnemigoSeguidorNivel5 : MonoBehaviour
{
    private PlayableGraph graph;
    [SerializeField] private Transform jugador;
    [SerializeField] private float velocidadNormal = 2f;
    [SerializeField] private float incrementoVelocidad = 1f; 
    [SerializeField] private float velocidadMaxima = 10f;    
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
    }

    public void Reanudar()
    {
        activo = true;
        if (animator != null)
            animator.SetBool("caminar", true);
    }

    public void Acelerar()
    {
        velocidadActual += incrementoVelocidad;
        velocidadActual = Mathf.Min(velocidadActual, velocidadMaxima);
    }

    public void ResetearVelocidad()
    {
        velocidadActual = velocidadNormal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MenuGameOver gameOver = FindFirstObjectByType<MenuGameOver>();

            if (gameOver != null)
            {
                gameOver.MostrarGameOver(); 
            }

            Time.timeScale = 0f;
        }
    }

    public void Morir()
    {
        StartCoroutine(MorirDespuesDeTresSegundos());
    }

    private IEnumerator MorirDespuesDeTresSegundos()
    {
        yield return new WaitForSecondsRealtime(3f); // ⏳ Espera 3 segundos

        activo = false;

        // Mover al centro de la pantalla
        Vector3 centroPantalla = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        centroPantalla.z = transform.position.z;
        transform.position = centroPantalla;

        // Instanciar efecto de muerte
        if (efectoMuerte != null)
        {
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // 💀 Destruir enemigo
    }




    private void OnDestroy()
    {
        if (graph.IsValid())
        {
            graph.Destroy();
        }
    }


}
