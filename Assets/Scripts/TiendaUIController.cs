using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TiendaCanvasController : MonoBehaviour
{
    public static TiendaCanvasController instance;

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


    private void Awake()
    {
        // Singleton para evitar duplicados
        if (instance != null && instance != this)
        {
            Debug.LogWarning("🟠 Se detectó un TiendaCanvasController duplicado. Eliminando...");
            Destroy(gameObject);
            return;
        }

        instance = this;
        
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

        Debug.Log($"🟢 TiendaCanvasController activo. ID: {GetInstanceID()}");

        var todos = FindObjectsByType<TiendaCanvasController>(FindObjectsSortMode.None);

        Debug.Log($"🧪 Total de TiendaCanvasControllers en escena: {todos.Length}");
    }

    void Comprar(int precio, string nombre)
    {
        if (nombre == "Vida")
        {
            Debug.Log($"💡 Intentando comprar vida");
            Debug.Log($"👀 SaludPersonaje.vidas = {SaludPersonaje.instance?.vidas}");
            Debug.Log($"📦 GameManager.VidasGuardadas = {GameManager.Instance.VidasGuardadas}");

            if (!GameManager.Instance.PuedeComprarVida())
            {
                MostrarMensaje("Max lives reached");
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

                MostrarMensaje("Extra life acquired!");
                ActualizarUI();
            }
            else
            {
                MostrarMensaje("Need more coins!");
            }
        }

        if (nombre == "Mejora")
        {
            if (GameManager.Instance.DañoBala >= 100f)
            {
                MostrarMensaje("Damage is at max.");
                return;
            }

            int precioActual = GameManager.Instance.CostoMejoraBala;

            if (GameManager.Instance.GastarMonedas(precioActual))
            {
                GameManager.Instance.MejorarBala();
                MostrarMensaje($"New damage: {GameManager.Instance.DañoBala}");
                ActualizarUI();
            }
            else
            {
                MostrarMensaje("Need more coins!");
            }
        }
    }

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

    void MostrarMensaje(string mensaje)
    {
        if (mensajeTexto != null)
        {
            mensajeTexto.text = mensaje;
            CancelInvoke(nameof(LimpiarMensaje));
            Invoke(nameof(LimpiarMensaje), 3f);
        }
    }

    void LimpiarMensaje()
    {
        mensajeTexto.text = "";
    }

    public void AbrirTienda()
    {
        Debug.Log("🛒 Abriendo tienda");
        canvasTienda.SetActive(true);
        // Ocultar HUD uno por uno
        if (botonTienda != null) botonTienda.SetActive(false);
        if (textoPuntaje != null) textoPuntaje.SetActive(false);
        if (textoVidas != null) textoVidas.SetActive(false);
        if (textoMonedas != null) textoMonedas.SetActive(false);
        if (botonPausa != null) botonPausa.SetActive(false);
        Time.timeScale = 0f;
        ActualizarUI(); // Actualiza los botones al abrir
    }

    public void CerrarTienda()
    {
        Debug.Log("🔙 Cerrando tienda");
        canvasTienda.SetActive(false);
        if (botonTienda != null) botonTienda.SetActive(true);
        if (textoPuntaje != null) textoPuntaje.SetActive(true);
        if (textoVidas != null) textoVidas.SetActive(true);
        if (textoMonedas != null) textoMonedas.SetActive(true);
        if (botonPausa != null) botonPausa.SetActive(true);
        Time.timeScale = 1f;
    }
}
