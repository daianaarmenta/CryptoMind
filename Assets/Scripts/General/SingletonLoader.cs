using UnityEngine;

/// <summary>
/// Clase base genérica que asegura que solo exista una instancia del componente.
/// Ideal para usar como base en GameManager, VidasHUD, etc.
/// </summary>
public class SingletonLoader<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    [SerializeField] private bool persistBetweenScenes = true;

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
                Debug.LogWarning("🟥 Duplicado detectado y destruido: " + typeof(T));
            Destroy(gameObject);
            return;
        }

        Instance = this as T;

        if (persistBetweenScenes)
        {
            DontDestroyOnLoad(gameObject);
        }

        Debug.Log("✅ SingletonLoader activo: " + typeof(T));
    }
}
