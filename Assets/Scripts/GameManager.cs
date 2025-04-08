using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int PuntosTotales { get { return puntosTotales; } }
    private int puntosTotales = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional si quieres que sobreviva entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Cargar desde PlayerPrefs
        puntosTotales = PlayerPrefs.GetInt("NumeroMonedas", 0);
    }

    public void SumarPuntos(int puntosAsumar)
    {
        puntosTotales += puntosAsumar;
        PlayerPrefs.SetInt("NumeroMonedas", puntosTotales);
        PlayerPrefs.Save();
    }
}
