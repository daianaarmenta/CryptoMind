using UnityEngine.Playables;
using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

/*
Autor: María Fernanda Pineda Pat
Comportamiento de un enemigo que sigue al jugador en el Nivel 5.
Puede acelerar, detenerse, morir con efectos visuales y controlar la cámara temporalmente.
*/
public class EnemigoSeguidorNivel5 : MonoBehaviour
{
    private PlayableGraph graph;
    [SerializeField] private Transform jugador;
    [SerializeField] private float velocidadNormal = 2f;
    [SerializeField] private float incrementoVelocidad = 1f; 
    [SerializeField] private float velocidadMaxima = 10f;    
    [SerializeField] private GameObject efectoMuerte;
    [SerializeField] private AudioClip efectoSonidoMuerte;
    private AudioSource audioSource;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    private CinemachineCamera cinemachineCamera;
    private float velocidadActual;
    private bool activo = true;
    private Animator animator;
 
    // Inicialización del enemigo. 
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

        audioSource = GetComponent<AudioSource>();
        cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
        
    }

    // Movimiento constante del enemigo hacia el jugador.
    private void Update()
    {
        if (!activo || jugador == null) return;

        Vector3 direccion = new Vector3(jugador.position.x - transform.position.x, 0f, 0f).normalized;
        transform.position += direccion * velocidadActual * Time.deltaTime;
    }

    // Detiene el movimiento y animación del enemigo.
    public void Detener()
    {
        activo = false;
        if (animator != null)
            animator.SetBool("caminar", false);
    }

    // Reanuda el movimiento y animación del enemigo.
    public void Reanudar()
    {
        activo = true;
        if (animator != null)
            animator.SetBool("caminar", true);
    }

    // Aumenta la velocidad del enemigo hasta un límite máximo.
    public void Acelerar()
    {
        velocidadActual += incrementoVelocidad;
        velocidadActual = Mathf.Min(velocidadActual, velocidadMaxima);
    }

    // Restablece la velocidad a la velocidad base.
    public void ResetearVelocidad()
    {
        velocidadActual = velocidadNormal;
    }

    // Si el enemigo colisiona con el jugador, activa Game Over.
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

    // Ejecuta la secuencia de muerte del enemigo con cámara, sonido y efectos visuales.
    public void Morir()
    {
        activo = false;
        cinemachineCamera.Follow = transform;
        cinemachineCamera.LookAt = transform;
        StartCoroutine(MorirDespuesDeTresSegundos());
    }

    // Corrutina que maneja efectos visuales y destrucción del enemigo tras morir.
    private IEnumerator MorirDespuesDeTresSegundos()
    {
        yield return new WaitForSecondsRealtime(1f);

        // Reproducir sonido en objeto aparte
        if (efectoSonidoMuerte != null)
        {
            GameObject audioGO = new GameObject("SonidoMuerte");
            AudioSource tempAudio = audioGO.AddComponent<AudioSource>();
            tempAudio.clip = efectoSonidoMuerte;
            tempAudio.Play();
            Destroy(audioGO, efectoSonidoMuerte.length);
        }

        // Instanciar efecto visual de muerte
        if (efectoMuerte != null)
        {
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        }

        // Desactivar el enemigo visualmente no lógicamente
        GetComponent<SpriteRenderer>().enabled = false;

        foreach (Collider2D col in GetComponents<Collider2D>())
        {
            col.enabled = false;
        }

        if (animator != null)
        {
            animator.enabled = false;
        }

        // Regresar cámara al jugador
        yield return new WaitForSecondsRealtime(0.5f);
        cinemachineCamera.Follow = jugador;
        cinemachineCamera.LookAt = jugador;

        // Finalmente destruir enemigo
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(gameObject);
    }

    // impia el PlayableGraph si fue usado (buena práctica).
    private void OnDestroy()
    {
        if (graph.IsValid())
        {
            graph.Destroy();
        }
    }

}
