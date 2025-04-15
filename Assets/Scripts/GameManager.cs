using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int monedas = 0;
    private int puntaje = 0;

    public int Monedas => monedas;
    public int Puntaje => puntaje;

    // 🔧 NUEVO: máximo de vidas permitido
    public int MaxVidas => 5;

    // ✅ Vidas guardadas persistentemente (inicia con 5 por defecto)
    public int VidasGuardadas
    {
        get => PlayerPrefs.GetInt("Vidas", MaxVidas); // 🟢 Siempre inicia con 5
        set
        {
            PlayerPrefs.SetInt("Vidas", Mathf.Clamp(value, 0, MaxVidas)); // 🔐 Nunca más de 5
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("🟩 GameManager activo");
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // 🔁 Cargar monedas y puntaje guardado al iniciar
        monedas = PlayerPrefs.GetInt("NumeroMonedas", 0);
        puntaje = PlayerPrefs.GetInt("Puntaje", 0);
    }

    // ✅ MONEDAS
    public void SumarMonedas(int cantidad)
    {
        monedas += cantidad;
        PlayerPrefs.SetInt("NumeroMonedas", monedas);
        PlayerPrefs.Save();
        Debug.Log("🪙 Monedas: " + monedas);
    }

    public bool TieneMonedasSuficientes(int cantidad) => monedas >= cantidad;

    public bool GastarMonedas(int cantidad)
    {
        if (TieneMonedasSuficientes(cantidad))
        {
            monedas -= cantidad;
            PlayerPrefs.SetInt("NumeroMonedas", monedas);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

    public void ReiniciarMonedas()
    {
        monedas = 0;
        PlayerPrefs.SetInt("NumeroMonedas", 0);
        PlayerPrefs.Save();
    }

    // ✅ PUNTAJE
    public void SumarPuntaje(int cantidad)
    {
        puntaje += cantidad;
        PlayerPrefs.SetInt("Puntaje", puntaje);
        PlayerPrefs.Save();
        Debug.Log("🏆 Puntaje: " + puntaje);
    }

    public void ReiniciarPuntaje()
    {
        puntaje = 0;
        PlayerPrefs.SetInt("Puntaje", 0);
        PlayerPrefs.Save();
    }

    // ✅ VIDAS
    public bool PuedeComprarVida() => VidasGuardadas < MaxVidas;

    public void ComprarVida()
    {
        if (PuedeComprarVida())
        {
            VidasGuardadas++;
            Debug.Log("❤️ Vida comprada. Total ahora: " + VidasGuardadas);
        }
        else
        {
            Debug.LogWarning("❌ Ya tienes el máximo de vidas.");
        }
    }

    public void ReiniciarVidas()
    {
        VidasGuardadas = MaxVidas;
    }
}
