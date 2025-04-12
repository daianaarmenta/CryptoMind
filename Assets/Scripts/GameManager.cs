using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int PuntosTotales { get { return puntosTotales; } }
    private int puntosTotales = 0;

    public int VidasGuardadas
    {
        get => PlayerPrefs.GetInt("Vidas", 3); // Por defecto inicia con 3
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
            DontDestroyOnLoad(gameObject); // Persistente entre escenas
            Debug.Log("🟩 GameManager activo");
        }
        else
        {
            Destroy(gameObject); // Evitar duplicados
            Debug.Log("🟥 GameManager duplicado destruido");
        }
    }

    private void Start()
{
    // Cargar monedas desde PlayerPrefs
    puntosTotales = PlayerPrefs.GetInt("NumeroMonedas", 0);
    Debug.Log("🟡 Monedas cargadas en GameManager: " + puntosTotales);

    // BONUS: Verificar si hay más de un GameManager
    if (Object.FindObjectsByType<GameManager>(FindObjectsSortMode.None).Length > 1)
{
    Debug.LogWarning("🚨 Hay más de un GameManager en escena. Elimina los duplicados.");
}

}


    public void SumarPuntos(int puntosAsumar)
    {
        puntosTotales += puntosAsumar;
        PlayerPrefs.SetInt("NumeroMonedas", puntosTotales);
        PlayerPrefs.Save();
        Debug.Log("🟢 Monedas después de recoger: " + puntosTotales);
    }

    public bool TieneMonedasSuficientes(int cantidad)
    {
        return puntosTotales >= cantidad;
    }

    public bool GastarMonedas(int cantidad)
    {
        if (TieneMonedasSuficientes(cantidad))
        {
            puntosTotales -= cantidad;
            PlayerPrefs.SetInt("NumeroMonedas", puntosTotales);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

    public bool PuedeComprarVida()
    {
        return VidasGuardadas < 3;
    }

    public void ComprarVida()
    {
        if (PuedeComprarVida())
        {
            VidasGuardadas++;
            Debug.Log("❤️ Vida comprada. Vidas ahora: " + VidasGuardadas);
        }
    }

    public void ReiniciarMonedas()
    {
        puntosTotales = 0;
        PlayerPrefs.SetInt("NumeroMonedas", 0);
        PlayerPrefs.Save();
    }

    public void ReiniciarVidas()
    {
        VidasGuardadas = 3;
    }
}