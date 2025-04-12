using UnityEngine;

/// <summary>
/// GameManager persistente para manejar monedas, vidas y datos globales entre escenas.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int puntosTotales = 0;
    public int PuntosTotales => puntosTotales;

    public int VidasGuardadas
    {
        get => PlayerPrefs.GetInt("Vidas", 3); // Valor por defecto: 3
        set
        {
            PlayerPrefs.SetInt("Vidas", Mathf.Clamp(value, 0, 3));
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ❗ Persiste entre escenas
            Debug.Log("🟩 GameManager activo");
        }
        else
        {
            Debug.LogWarning("🟥 GameManager duplicado destruido");
            Destroy(gameObject); // ❌ Evita duplicados
            return;
        }

        // Revisión extra por si acaso
        if (FindObjectsByType<GameManager>(FindObjectsSortMode.None).Length > 1)
        {
            Debug.LogWarning("🚨 ¡Hay más de un GameManager en la escena!");
        }
    }

    private void Start()
    {
        // Cargar monedas desde PlayerPrefs
        puntosTotales = PlayerPrefs.GetInt("NumeroMonedas", 0);
        Debug.Log("🟡 Monedas cargadas: " + puntosTotales);
    }

    // ✅ Sumar monedas y guardar
    public void SumarPuntos(int cantidad)
    {
        puntosTotales += cantidad;
        PlayerPrefs.SetInt("NumeroMonedas", puntosTotales);
        PlayerPrefs.Save();
        Debug.Log("🟢 Monedas después de sumar: " + puntosTotales);
    }

    // Verificar si alcanza para comprar algo
    public bool TieneMonedasSuficientes(int cantidad) => puntosTotales >= cantidad;

    // Usar monedas
    public bool GastarMonedas(int cantidad)
    {
        if (TieneMonedasSuficientes(cantidad))
        {
            puntosTotales -= cantidad;
            PlayerPrefs.SetInt("NumeroMonedas", puntosTotales);
            PlayerPrefs.Save();
            Debug.Log("💸 Monedas después de gastar: " + puntosTotales);
            return true;
        }
        return false;
    }

    // Verifica si puede comprar vida
    public bool PuedeComprarVida() => VidasGuardadas < 3;

    // Comprar una vida
    public void ComprarVida()
    {
        if (PuedeComprarVida())
        {
            VidasGuardadas++;
            Debug.Log("❤️ Vida comprada. Total ahora: " + VidasGuardadas);
        }
    }

    // Reiniciar monedas (ej. al empezar juego nuevo)
    public void ReiniciarMonedas()
    {
        puntosTotales = 0;
        PlayerPrefs.SetInt("NumeroMonedas", 0);
        PlayerPrefs.Save();
    }

    // Reiniciar vidas (por si reinicias el juego)
    public void ReiniciarVidas() => VidasGuardadas = 3;
}
    