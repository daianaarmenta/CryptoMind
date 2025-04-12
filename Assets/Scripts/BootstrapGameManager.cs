using UnityEngine;

public class BootstrapGameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerPrefab;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            Debug.Log("🔄 Instanciando GameManager desde Bootstrapper.");
            Instantiate(gameManagerPrefab);
        }
        else
        {
            Debug.Log("✅ GameManager ya está presente.");
        }
    }
}
