using UnityEngine;

public class BootstrapVidasHUD : MonoBehaviour
{
    void Awake()
    {
        if (VidasHUD.instance == null)
        {
            GameObject prefab = Resources.Load<GameObject>("VidasHUD"); // Debe estar en /Resources
            if (prefab != null)
            {
                Instantiate(prefab);
                Debug.Log("🟢 VidasHUD instanciado desde Bootstrapper.");
            }
            else
            {
                Debug.LogError("❌ Prefab VidasHUD no encontrado en Resources.");
            }
        }
        else
        {
            Debug.Log("✅ VidasHUD ya presente en escena.");
        }
    }
}
    