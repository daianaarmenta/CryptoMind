using UnityEngine;
using UnityEngine.UIElements;

public class VidasHUD : MonoBehaviour
{
    public static VidasHUD instance;
    private VisualElement vida_1;
    private VisualElement vida_2;
    private VisualElement vida_3;
    private Button botonPerderVida; 
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        vida_1 = root.Q<VisualElement>("Vida_1");
        vida_2 = root.Q<VisualElement>("Vida_2");
        vida_3 = root.Q<VisualElement>("Vida_3");

        botonPerderVida = root.Q<Button>("Button");

        if (botonPerderVida != null)
        {
            botonPerderVida.clicked += PerderVida; 
        }

        ActualizarVidas();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ActualizarVidas()
    {
        if (SaludPersonaje.instance == null) return;

        int vidas = SaludPersonaje.instance.vidas;
        vida_1.style.visibility = (vidas >= 1) ? Visibility.Visible : Visibility.Hidden;
        vida_2.style.visibility = (vidas >= 2) ? Visibility.Visible : Visibility.Hidden;
        vida_3.style.visibility = (vidas >= 3) ? Visibility.Visible : Visibility.Hidden;

        Debug.Log("HUD actualizado, vidas actuales: " + vidas);
    }

    private void PerderVida()
    {
        if (SaludPersonaje.instance != null)
        {
            SaludPersonaje.instance.PerderVida();
            Debug.Log("Botón presionado: Se ha perdido una vida.");
        }
    }
}
