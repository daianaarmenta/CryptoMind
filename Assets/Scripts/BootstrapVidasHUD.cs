using UnityEngine;

public class BootstrapVidasHUD : MonoBehaviour
{
    [SerializeField] private GameObject vidasHUDPrefab;

    private void Awake()
    {
        // Si ya hay una instancia activa, no creamos otra
        if (VidasHUD.instance != null)
        {
            Debug.Log("🟢 VidasHUD ya existe en la escena.");
            return;
        }

        // Instancia el prefab desde el campo asignado
        if (vidasHUDPrefab != null)
        {
            GameObject hudInstanciado = Instantiate(vidasHUDPrefab);
            DontDestroyOnLoad(hudInstanciado);
            Debug.Log("✅ VidasHUD instanciado desde bootstrap.");
        }
        else
        {
            Debug.LogError("❌ No se ha asignado el prefab de VidasHUD en el inspector.");
        }
    }
}
