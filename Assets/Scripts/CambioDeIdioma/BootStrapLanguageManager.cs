using UnityEngine;

public class BootstrapLanguageManager : MonoBehaviour
{
    [SerializeField] private GameObject LanguageManager;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            Debug.Log("🔄 Instanciando GameManager desde Bootstrapper.");
            Instantiate(LanguageManager);
        }
        else
        {
            Debug.Log("✅ GameManager ya está presente.");
        }
    }
}