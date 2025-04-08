using UnityEngine;
/*
Autor: Fernanda Pineda 
Este codigo es para gestionar el puntaje total del jugador y la persistencia de datos entre escenas
*/
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
            DontDestroyOnLoad(gameObject);
            Debug.Log("🟩 GameManager activo");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("🟥 GameManager duplicado destruido");
        }
    }

    private void Start()
    {
        puntosTotales = PlayerPrefs.GetInt("NumeroMonedas", 0);
        Debug.Log("🟡 Monedas cargadas en GameManager: " + puntosTotales);
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
}
