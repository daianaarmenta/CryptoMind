using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

/*
Autor: María Fernanda Pineda Pat
Controla la tienda dentro del juego , permitiendo comprar vidas y mejoras.
Tambien maneja la UI de la tienda, sonidos y la logica de apertura/cierre del canvas. 
*/
public class TiendaCanvasController : MonoBehaviour
{
    public static TiendaCanvasController instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoCompra;
    [SerializeField] private AudioClip sonidoError;
    [SerializeField] private AudioSource musicaFondo;
    [SerializeField] private AudioClip musicaTienda;

    [Header("Referencias UI")]
    public TextMeshProUGUI monedasTexto;
    public TextMeshProUGUI precioMejoraTexto;
    public TextMeshProUGUI mensajeTexto;

    public Button botonVida;
    public Button botonMejora;

    [Header("Canvases")]
    [SerializeField] private GameObject canvasHUD;
    [SerializeField] private GameObject canvasTienda;
    [Header("Elementos del HUD que se deben ocultar")]
    [SerializeField] private GameObject botonTienda;
    [SerializeField] private GameObject textoPuntaje;
    [SerializeField] private GameObject textoVidas;
    [SerializeField] private GameObject textoMonedas;
    [SerializeField] private GameObject botonPausa;


    // Establece el Singleton y obtiene la fuente de audio del objeto
    private void Awake()
    {

        // Singleton para evitar duplicados
        if (instance != null && instance != this)
        {
            //Debug.LogWarning("Se detectó un TiendaCanvasController duplicado. Eliminando...");
            Destroy(gameObject);
            return;
        }

        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (botonVida != null)
        {
            botonVida.onClick.RemoveAllListeners(); // Limpia duplicados
            botonVida.onClick.AddListener(() => Comprar(100, "Vida"));
        }

        if (botonMejora != null)
        {
            botonMejora.onClick.RemoveAllListeners();
            botonMejora.onClick.AddListener(() => Comprar(GameManager.Instance.CostoMejoraBala, "Mejora"));
        }

        ActualizarUI();

        //Debug.Log($" TiendaCanvasController activo. ID: {GetInstanceID()}");

        var todos = FindObjectsByType<TiendaCanvasController>(FindObjectsSortMode.None);

        //Debug.Log($" Total de TiendaCanvasControllers en escena: {todos.Length}");
    }

    // Lógica para comprar una vida o una mejora, segun el nombre y el precio indicado
    void Comprar(int precio, string nombre)
    {
        if (nombre == "Vida")
        {
            //Debug.Log($" Intentando comprar vida");
            //Debug.Log($" SaludPersonaje.vidas = {SaludPersonaje.instance?.vidas}");
            //Debug.Log($" GameManager.VidasGuardadas = {GameManager.Instance.VidasGuardadas}");

            if (!GameManager.Instance.PuedeComprarVida())
            {
                MostrarMensaje("Max lives reached");
                Invoke(nameof(LimpiarMensaje), 3f);

                return;
            }

            if (GameManager.Instance.GastarMonedas(precio))
            {
                GameManager.Instance.ComprarVida();

                if (SaludPersonaje.instance != null)
                {
                    SaludPersonaje.instance.vidas = GameManager.Instance.VidasGuardadas;
                    VidasHUD.instance?.ActualizarVidas();
                }
                audioSource.PlayOneShot(sonidoCompra, 1f);

                MostrarMensaje("Extra life acquired!");
                Invoke(nameof(LimpiarMensaje), 3f);

                ActualizarUI();
            }
            else
            {
                audioSource.PlayOneShot(sonidoError, 1f);
                MostrarMensaje("Need more coins!");
                Invoke(nameof(LimpiarMensaje), 3f);

            }
        }

        if (nombre == "Mejora")
        {
            if (GameManager.Instance.DañoBala >= 100f)
            {
                MostrarMensaje("Damage is at max.");
                Invoke(nameof(LimpiarMensaje), 3f);

                return;
            }

            int precioActual = GameManager.Instance.CostoMejoraBala;

            if (GameManager.Instance.GastarMonedas(precioActual))
            {
                GameManager.Instance.MejorarBala();
                audioSource.PlayOneShot(sonidoCompra, 1f);
                MostrarMensaje($"New damage: {GameManager.Instance.DañoBala}");
                Invoke(nameof(LimpiarMensaje), 3f);

                ActualizarUI();
            }
            else
            {
                audioSource.PlayOneShot(sonidoError, 1f);
                MostrarMensaje("Need more coins!");
                Invoke(nameof(LimpiarMensaje), 3f);

            }
        }
    }

    // Actualiza los textos y botones de la tienda según el estado actual del jugador.
    void ActualizarUI()
    {
        monedasTexto.text = GameManager.Instance.Monedas.ToString();

        if (GameManager.Instance.DañoBala >= 100f)
        {
            precioMejoraTexto.text = "MAX";
            botonMejora.interactable = false;
        }
        else
        {
            precioMejoraTexto.text = GameManager.Instance.CostoMejoraBala.ToString();
            botonMejora.interactable = true;
        }

        botonVida.interactable = GameManager.Instance.PuedeComprarVida();
    }

    // Muestra un mensaje temporal enla tienda.
    void MostrarMensaje(string mensaje)
    {
        if (mensajeTexto != null)
        {
            mensajeTexto.text = mensaje;
            CancelInvoke(nameof(LimpiarMensaje));
            Invoke(nameof(LimpiarMensaje), 3f);
        }
    }

    // Limpia el mensaje mostrado en la tienda.
    void LimpiarMensaje()
    {
        mensajeTexto.text = "";
    }

    // Activa la tienda, pausa el juego y oculta el HUD.
    public void AbrirTienda()
    {
        //Debug.Log(" Abriendo tienda");
        canvasTienda.SetActive(true);
        musicaFondo.Pause();
        audioSource.PlayOneShot(musicaTienda, 1f); // Reproducir música de tienda
        // Ocultar HUD uno por uno
        if (botonTienda != null) botonTienda.SetActive(false);
        if (textoPuntaje != null) textoPuntaje.SetActive(false);
        if (textoVidas != null) textoVidas.SetActive(false);
        if (textoMonedas != null) textoMonedas.SetActive(false);
        if (botonPausa != null) botonPausa.SetActive(false);
        Time.timeScale = 0f;
        ActualizarUI(); // Actualiza los botones al abrir
    }

    // Cierra la tienda, reanuda el juego y muestra el HUD.
    public void CerrarTienda()
    {
        //Debug.Log(" Cerrando tienda");
        canvasTienda.SetActive(false);
        audioSource.Stop();
        musicaFondo.UnPause(); // Reanudar música de fondo
        if (botonTienda != null) botonTienda.SetActive(true);
        if (textoPuntaje != null) textoPuntaje.SetActive(true);
        if (textoVidas != null) textoVidas.SetActive(true);
        if (textoMonedas != null) textoMonedas.SetActive(true);
        if (botonPausa != null) botonPausa.SetActive(true);
        Time.timeScale = 1f;
    }
}
