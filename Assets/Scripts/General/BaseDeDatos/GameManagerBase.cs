using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerBase : MonoBehaviour
{
    public static GameManagerBase Instance { get; private set; }

    public bool VolviendoDeTienda { get; set; } = false;

    public float DañoBala { get; private set; } = 20f;
    public int CostoMejoraBala { get; private set; } = 25;

    public int MaxVidas => 5;

    // Vidas locales (puedes mantenerlas si no las manejas desde servidor)
    private int vidas = 5;
    public int VidasGuardadas
    {
        get => vidas;
        set => vidas = Mathf.Clamp(value, 0, MaxVidas);
    }

    private int monedas = 0;
    private int puntaje = 0;

    public int Monedas => monedas;
    public int Puntaje => puntaje;

    // Datos de usuario desde servidor
    public int idUsuario { get; private set; }
    public string nombreUsuario { get; private set; }

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
        }
    }

    private void Start()
    {
        string escena = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (!escena.Contains("Nivel5") && !escena.Contains("5"))
        {
            ReiniciarVidas();
        }
    }

    public void CargarDatosDesdeServidor(int id, string nombre, int tokens, int score, float daño, int costo)
    {
        idUsuario = id;
        nombreUsuario = nombre;
        monedas = tokens;
        puntaje = score;
        DañoBala = daño;
        CostoMejoraBala = costo;

        Debug.Log("Datos del usuario cargados desde servidor.");
    }

    // ✅ MONEDAS
    public void SumarMonedas(int cantidad)
    {
        monedas += cantidad;
    }

    public bool TieneMonedasSuficientes(int cantidad) => monedas >= cantidad;

    public bool GastarMonedas(int cantidad)
    {
        if (TieneMonedasSuficientes(cantidad))
        {
            monedas -= cantidad;
            return true;
        }
        return false;
    }

    // ✅ PUNTAJE
    public void SumarPuntaje(int cantidad)
    {
        puntaje += cantidad;
        Debug.Log("🏆 Puntaje actualizado: " + puntaje);
    }

    public void ReiniciarPuntaje()
    {
        puntaje = 0;
    }

    // ✅ VIDAS
    public bool PuedeComprarVida() => VidasGuardadas < MaxVidas;

    public void ComprarVida()
    {
        if (PuedeComprarVida())
        {
            VidasGuardadas++;
            Debug.Log("Vida comprada. Total: " + VidasGuardadas);
        }
        else
        {
            Debug.LogWarning("Ya tienes el máximo de vidas.");
        }
    }

    public void ReiniciarVidas()
    {
        VidasGuardadas = MaxVidas;
        if (SaludPersonaje.instance != null)
        {
            SaludPersonaje.instance.vidas = MaxVidas;
        }
        Debug.Log("🔁 Vidas reiniciadas.");
    }

    // ✅ MEJORAS
    public void MejorarBala()
    {
        if (DañoBala < 100)
        {
            DañoBala = Mathf.Min(DañoBala + 5f, 100f);
            CostoMejoraBala += 25;
            Debug.Log($"💥 Daño mejorado: {DañoBala} | Nuevo costo: {CostoMejoraBala}");
        }
        else
        {
            Debug.Log("⚠️ Daño máximo alcanzado.");
        }
    }

    // ✅ Utilidad
    public static void LimpiarPosicionJugador()
    {
        // Esto es útil si lo manejas localmente
        Debug.Log("📍 Posición del jugador limpiada.");
    }

    public IEnumerator GuardarProgresoEnServidor(System.Action alTerminar = null)
    {
        var progreso = new ProgresoJugador
        {
            id_usuario = idUsuario,
            nombre = nombreUsuario,
            tokens = Monedas,
            puntaje = Puntaje,
            daño_bala = DañoBala,
            costo_mejora = CostoMejoraBala
        };

        string json = JsonUtility.ToJson(progreso);
        Debug.Log("JSON que se envía: " + json);
        UnityWebRequest request = new UnityWebRequest(ServidorConfig.GuardarProgreso, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Progreso enviado correctamente al servidor.");
        }
        else
        {
            Debug.LogError("Error al guardar progreso: " + request.error);
        }

        alTerminar?.Invoke(); // Llamar al callback al final
    }

    public void CerrarSesion()
    {
        idUsuario = 0;
        nombreUsuario = "";
        monedas = 0;
        puntaje = 0;
        DañoBala = 20f;
        CostoMejoraBala = 25;
        ReiniciarVidas();
        Debug.Log("🚪 Sesión cerrada correctamente.");
    }
}
