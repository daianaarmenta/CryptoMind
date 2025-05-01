using UnityEngine;
using UnityEngine.UI;

public class LogicaBrillo : MonoBehaviour
{
    public Slider sliderBrillo; // Referencia al slider de brillo
    public float sliderValue;   // Valor del brillo
    public Image panelBrillo;   // Referencia al panel que oscurece o aclara la pantalla

    void Start()
    {
        // Carga el valor de brillo guardado
        sliderValue = PlayerPrefs.GetFloat("brillo", 0.5f);
        sliderBrillo.value = sliderValue;

        // Aplica el brillo invertido para que la izquierda sea oscuro
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, 1 - sliderValue);
    }

    public void CambiarBrillo(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("brillo", sliderValue); // Guarda el valor
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, 1 - sliderValue);
    }
}
