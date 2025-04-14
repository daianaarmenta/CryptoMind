using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class alien : MonoBehaviour
{
    [SerializeField] private int cantidadCheckpoint;
    [SerializeField] private int checkpointTerminado;
    [SerializeField] private TextMeshProUGUI mensajeTexto; // ← Asigna desde el inspector

    void Start()
    {
        cantidadCheckpoint = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        checkpointTerminado = 0;

        if (mensajeTexto != null)
        {
            mensajeTexto.gameObject.SetActive(false); // Oculta el texto al inicio
        }
        else
        {
            Debug.LogWarning("⚠️ No se asignó mensajeTexto en el Inspector.");
        }
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
        if (mensajeTexto != null)
        {
            mensajeTexto.gameObject.SetActive(true);
            mensajeTexto.text = texto;
            CancelInvoke(nameof(EsconderMensaje));
            Invoke(nameof(EsconderMensaje), 2f); // Oculta el mensaje en 2 segundos
        }
    }

    private void EsconderMensaje()
    {
        if (mensajeTexto != null)
        {
            mensajeTexto.gameObject.SetActive(false);
        }
    }
}
