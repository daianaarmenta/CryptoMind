using UnityEngine;
using UnityEngine.UI;
public class LogicaVolumen : MonoBehaviour
{
    public Slider sliderVolumen;
    public float sliderValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderVolumen.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f); // Carga el volumen guardado o establece el valor predeterminado en 0.5f
        AudioListener.volume = sliderVolumen.value; // Establece el volumen inicial del AudioListener

    }
    public void CambiarVolumen(float valor)
    {
        sliderValue = valor; // Actualiza el valor del volumen
        PlayerPrefs.SetFloat("volumenAudio", sliderValue); // Guarda el volumen en PlayerPrefs
        AudioListener.volume = sliderValue; // Establece el volumen del AudioListener
    }
}
