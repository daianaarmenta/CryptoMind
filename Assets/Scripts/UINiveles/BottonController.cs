using UnityEngine;
using UnityEngine.SceneManagement;



public class BottonController : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private GameObject botonPausa;     // Botón que abre el menú de pausa
    [SerializeField] private GameObject menuPausa;      // Menú de pausa completo
    [SerializeField] private GameObject canvasTienda;
    [SerializeField] private AudioSource musicaFondo;  // Canvas de la tienda
    [SerializeField] private GameObject panelOpciones;
    private AudioSource audioSource; // Fuente de audio para reproducir música

    // 🔘 PAUSAR el juego

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        
    }
    public void Pausa()
    {
        Time.timeScale = 0f;                // Pausa el tiempo del juego
        botonPausa.SetActive(false);        // Oculta el botón de pausa
        menuPausa.SetActive(true);
        musicaFondo.Pause();          // Muestra el menú de pausa
    }

    // ▶️ REANUDAR el juego
    public void Reanudar()
    {
        Time.timeScale = 1f;                // Reanuda el tiempo del juego
        botonPausa.SetActive(true);         // Muestra el botón de pausa
        menuPausa.SetActive(false);
        musicaFondo.UnPause();         // Oculta el menú de pausa
    }

    // 🔄 REINICIAR el nivel completo
    public void Reiniciar()
    {
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarVidas();          // Reinicia las vidas
            GameManager.Instance.VolviendoDeTienda = false; // No viene de tienda
        }

        // Limpia posición guardada
        PlayerPrefs.DeleteKey("JugadorX");
        PlayerPrefs.DeleteKey("JugadorY");
        PlayerPrefs.DeleteKey("JugadorZ");
        PlayerPrefs.Save();

        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 🛒 ABRIR tienda (como menú de pausa)
    public void Tienda()
    {
        Time.timeScale = 0f;                // Pausar el juego
        botonPausa.SetActive(false);        // Ocultar botón de pausa
        canvasTienda.SetActive(true);       // Mostrar canvas de tienda
    }

    // ❌ CERRAR tienda
    public void CerrarTienda()
    {
        Time.timeScale = 1f;                // Reanudar el juego
        botonPausa.SetActive(true);         // Mostrar botón de pausa
        canvasTienda.SetActive(false);      // Ocultar tienda
    }

    // 🚪 SALIR al menú principal
    public void Salir()
    {
        Time.timeScale = 1f;                // Asegura que el tiempo esté normal
        SceneManager.LoadScene(1);          // Cambia a la escena del menú principal (index 1)
    }
    public void AbrirOpciones()
    {
        menuPausa.SetActive(false); // Oculta el menú de pausa
        panelOpciones.SetActive(true); // Muestra el panel de opciones
    }
    public void CerrarOpciones()
    {
        menuPausa.SetActive(true); // Muestra el menú de pausa
        panelOpciones.SetActive(false); // Oculta el panel de opciones
    }

}
