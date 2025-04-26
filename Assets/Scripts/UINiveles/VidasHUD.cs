using UnityEngine;
using TMPro;

public class VidasHUD : MonoBehaviour
{
    public static VidasHUD instance;

    [SerializeField] private TextMeshProUGUI vidasTexto;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ActualizarVidas();
    }

    public void ActualizarVidas()
    {
        if (SaludPersonaje.instance == null)
        {
            Debug.LogWarning("⚠️ No se puede actualizar HUD, SaludPersonaje no está listo.");
            return;
        }

        int vidas = SaludPersonaje.instance.vidas;

        if (vidasTexto != null)
        {
            vidasTexto.text = $" {vidas}";
            Debug.Log("✅ Texto de vidas actualizado: " + vidas);
        }
        else
        {
            Debug.LogWarning("⚠️ El campo de texto no está asignado.");
        }
    }

    public void ReiniciarVidas()
    {
        if (SaludPersonaje.instance != null)
        {
            SaludPersonaje.instance.vidas = SaludPersonaje.instance.vidasMaximas;
            ActualizarVidas();
        }
    }

    // ✅ NUEVO: Método para actualizar desde GameManager (por ejemplo, desde la tienda)
    public void SetVidasDesdeGameManager()
    {
        if (vidasTexto != null)
        {
            int vidas = GameManager.Instance.VidasGuardadas;
            vidasTexto.text = $" {vidas}";
            Debug.Log("✅ Texto de vidas actualizado desde GameManager: " + vidas);
        }
    }
}
