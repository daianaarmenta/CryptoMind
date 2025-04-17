using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool VolviendoDeTienda { get; set; } = false; // 🏪 Indica si se está volviendo de la tienda
    public float DañoBala{ get; private set;}= 20f;
    public int CostoMejoraBala{ get; private set;}= 25;

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
        

        //Para reiniciar daño   
        //PlayerPrefs.DeleteKey("DañoBala");
        //PlayerPrefs.DeleteKey("CostoMejora");
        PlayerPrefs.Save();
        // 🔁 Cargar monedas y puntaje guardado al iniciar
        monedas = PlayerPrefs.GetInt("NumeroMonedas", 0);
        puntaje = PlayerPrefs.GetInt("Puntaje", 0);

        DañoBala = PlayerPrefs.GetFloat("DañoBala", 20f); // Empieza en 20 por defecto
        CostoMejoraBala = PlayerPrefs.GetInt("CostoMejora", 25); // Empieza en 50 por defecto
        ReiniciarVidas();
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

    public static void LimpiarPosicionJugador()
    {
        PlayerPrefs.DeleteKey("JugadorX");
        PlayerPrefs.DeleteKey("JugadorY");
        PlayerPrefs.DeleteKey("JugadorZ");
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

        if (SaludPersonaje.instance != null)
        {
            SaludPersonaje.instance.vidas = MaxVidas; // 🔁 sincroniza variable local
        }

        Debug.Log("🔁 Vidas reiniciadas a: " + MaxVidas);
    }

    public void MejorarBala(){
        if( DañoBala <100 ){
            DañoBala = Mathf.Min(DañoBala + 5f, 100f);
            CostoMejoraBala += 25;  
            PlayerPrefs.SetFloat("DañoBala", DañoBala);
            PlayerPrefs.SetInt("CostoMejora", CostoMejoraBala);
            PlayerPrefs.Save();
            Debug.Log($"💥 Daño mejorado a: {DañoBala} | Costo siguiente: {CostoMejoraBala}");
        }else{
            Debug.Log("⚠️ Ya tienes el daño máximo.");
        }
    }

}   
