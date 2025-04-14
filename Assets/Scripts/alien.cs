using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class alien : MonoBehaviour
{
    [SerializeField] private int cantidadCheckpoint;
    [SerializeField] private int checkpointTerminado;

    [Header("UI de mensaje")]
    [SerializeField] private GameObject panelMensaje; // ← El GameObject que incluye fondo + texto
    [SerializeField] private TextMeshProUGUI mensajeTexto; // ← El texto dentro del panel

    void Start()
    {
        cantidadCheckpoint = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        checkpointTerminado = 0;

        if (panelMensaje != null)
            panelMensaje.SetActive(false); // Oculta todo al inicio
        else
            Debug.LogWarning("⚠️ No se asignó el panel del mensaje en el Inspector.");
    }

    public void AumentarCheckpoints()
    {
        checkpointTerminado++;
        Debug.Log($"Checkpoints completados: {checkpointTerminado}/{cantidadCheckpoint}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (checkpointTerminado == cantidadCheckpoint)
            {
                Debug.Log("✅ Todos los checkpoints completados. Avanzando de escena...");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.Log("🚫 Aún faltan checkpoints por completar.");
                MostrarMensaje($"You are missing {cantidadCheckpoint - checkpointTerminado} checkpoints!");
            }
        }
    }

    private void MostrarMensaje(string texto)
    {
        if (panelMensaje != null && mensajeTexto != null)
        {
            panelMensaje.SetActive(true);
            mensajeTexto.text = texto;
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
