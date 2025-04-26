using UnityEngine;
using UnityEngine.UI;

public class LogicaBrillo : MonoBehaviour
{
    public Slider sliderBrillo; // Referencia al slider de brillo
    public float sliderValue; // Valor del brillo
    public Image panelBrillo; // Referencia al panel de opciones
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       sliderBrillo.value = PlayerPrefs.GetFloat("brillo", 0.5f); // Carga el brillo guardado o establece el valor predeterminado en 0.5f
       panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g,panelBrillo.color.b,sliderBrillo.value); // Establece el brillo inicial del panel de opciones
    }
    public void CambiarBrillo(float valor)
    {
        sliderValue = valor; // Actualiza el valor del brillo
        PlayerPrefs.SetFloat("brillo", sliderValue); // Guarda el brillo en PlayerPrefs
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g,panelBrillo.color.b,sliderBrillo.value); // Establece el brillo del panel de opciones
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
