using UnityEngine;

public class BootstrapLanguageManager : MonoBehaviour
{
    [SerializeField] private GameObject LanguageManager;

    private void Awake()
    {
        if (FindFirstObjectByType<LanguageManager>() == null)
        {
            Debug.Log("🔄 Instanciando LanguageManager desde Bootstrapper.");
            Instantiate(LanguageManager);
        }
        else
        {
            Debug.Log("✅ LanguageManager ya está presente.");
        }
    }
}