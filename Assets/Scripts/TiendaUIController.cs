using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TiendaCanvasController : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI monedasTexto;
    public TextMeshProUGUI precioMejoraTexto;
    public TextMeshProUGUI mensajeTexto;

    public Button botonVida;
    public Button botonMejora;

    [Header("Canvases")]
    [SerializeField] private GameObject canvasHUD;
    [SerializeField] private GameObject canvasTienda;

    void Start()
    {
        if (botonVida != null)
            botonVida.onClick.AddListener(() => Comprar(100, "Vida"));

        if (botonMejora != null)
            botonMejora.onClick.AddListener(() => Comprar(GameManager.Instance.CostoMejoraBala, "Mejora"));

        ActualizarUI();
    }

    void Comprar(int precio, string nombre)
    {
        if (nombre == "Vida")
        {
            if (!GameManager.Instance.PuedeComprarVida())
            {
                MostrarMensaje("Max lives reached");
                return;
            }

            if (GameManager.Instance.GastarMonedas(precio))
            {
                GameManager.Instance.ComprarVida();
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
            if (GameManager.Instance.DañoBala >= 50f)
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

        if (GameManager.Instance.DañoBala >= 50f)
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
            Invoke(nameof(LimpiarMensaje), 3f); // Borra mensaje a los 3 segundos
        }
    }

    void LimpiarMensaje()
    {
        mensajeTexto.text = "";
    }

    // 🛒 ABRIR tienda como menú
    public void AbrirTienda()
    {
        Debug.Log("🛒 Tienda activada");

        canvasTienda.SetActive(true);     // Mostrar la tienda
        canvasHUD.SetActive(false);       // Ocultar el HUD del juego
        Time.timeScale = 0f;              // Pausar el juego
    }

    // ❌ CERRAR tienda y reanudar el juego
    public void CerrarTienda()
    {
        Debug.Log("🔙 Cerrando tienda");

        canvasTienda.SetActive(false);    // Ocultar tienda
        canvasHUD.SetActive(true);        // Mostrar HUD
        Time.timeScale = 1f;              // Reanudar juego
    }
}
