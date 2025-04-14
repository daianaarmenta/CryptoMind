using UnityEngine;
using UnityEngine.SceneManagement;

public class alien : MonoBehaviour
{
    [SerializeField] private int cantidadCheckpoint;
    [SerializeField] private int checkpointTerminado;

    void Start()
    {
        cantidadCheckpoint = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        checkpointTerminado = 0;
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
            }
        }
    }
}
