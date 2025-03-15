using UnityEngine;
using UnityEngine.UI;

public class VidasHUD : MonoBehaviour
{
    public static VidasHUD instance;
    [SerializeField] Image vida_1;
    [SerializeField] Image vida_2;
    [SerializeField] Image vida_3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir el HUD al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ActualizarVidas();
    }

    public void ActualizarVidas()
    {
        if (SaludPersonaje.instance == null) return;

        int vidas = SaludPersonaje.instance.vidas;
        vida_1.gameObject.SetActive(vidas >=1);
        vida_2.gameObject.SetActive(vidas >=2);
        vida_3.gameObject.SetActive(vidas >=3);
        Debug.Log("HUD actualizado, vidas actuales: " + vidas);
    }
}
