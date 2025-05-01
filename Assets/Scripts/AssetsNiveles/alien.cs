using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Alien : MonoBehaviour
{
    [SerializeField] public int cantidadCheckpoint {get; private set;}
    [SerializeField] public int checkpointTerminado {get; private set;} // Propiedad para acceder al número de checkpoints terminados desde fuera de la clase;
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip errorClip;
    [SerializeField] private AudioClip siguienteNivel;

    [Header("UI de mensaje")]
    [SerializeField] private GameObject panelMensaje; // ← El GameObject que incluye fondo + texto
    [SerializeField] private TextMeshProUGUI mensajeTexto; // ← El texto dentro del panel

    void Start()
    {
        cantidadCheckpoint = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        checkpointTerminado = 0;

        if (panelMensaje != null)
            panelMensaje.SetActive(false); // Oculta el mensaje al inicio
        else
            Debug.LogWarning("⚠️ No se asignó el panel del mensaje en el Inspector.");
    }

    public void AumentarCheckpoints()
    {
        checkpointTerminado++;
        Debug.Log($"Desde alien: ✅ Checkpoints completados: {checkpointTerminado}/{cantidadCheckpoint}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (checkpointTerminado == cantidadCheckpoint)
            {
                audiosource.PlayOneShot(siguienteNivel);
                // ✅ Reiniciar vidas para el siguiente nivel
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.ReiniciarVidas();
                    Debug.Log("🔄 Vidas reiniciadas para el siguiente nivel.");
                }

                // ✅ Cambiar de escena con o sin transición
                TransicionEscena transicion = FindFirstObjectByType<TransicionEscena>();
                if (transicion != null)
                {

                    transicion.IrASiguienteEscena();
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else
            {
                int restantes = cantidadCheckpoint - checkpointTerminado;
                string mensaje = LanguageManager.instance.GetFormattedText("missing_checkpoints", restantes);
                MostrarMensaje(mensaje);
            }
        }
    }

    private void MostrarMensaje(string texto)
    {
        if (panelMensaje != null && mensajeTexto != null)
        {
            panelMensaje.SetActive(true);
            mensajeTexto.text = texto;
            audiosource.PlayOneShot(errorClip);
            CancelInvoke(nameof(EsconderMensaje));
            Invoke(nameof(EsconderMensaje), 2f);
        }
    }

    private void EsconderMensaje()
    {
        if (panelMensaje != null)
        {
            panelMensaje.SetActive(false);
        }
    }
}