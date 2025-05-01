using UnityEngine;
using UnityEngine.UI;
/*
Autor: María Fernanda Pineda Pat
Controla el volumen general del juego mediante un slider de UI. El valor del volumen se guarda en PlayerPrefs
*/
public class LogicaVolumen : MonoBehaviour
{
    public Slider sliderVolumen; //Slider de la interfaz de usuario
    public float sliderValue; // Valor actual del volumen
    
    //Se ejecuta al iniciar el juego. Carga el volumen guardado y lo aplica.
    void Start()
    {
        sliderVolumen.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f); // Carga el volumen guardado o establece el valor predeterminado en 0.5f
        AudioListener.volume = sliderVolumen.value; // Establece el volumen inicial del AudioListener

    }

    // Se llama cuando se modifica el valor del slider. Actualiza y guarda el volumen. 
    public void CambiarVolumen(float valor)
    {
        sliderValue = valor; // Actualiza el valor del volumen
        PlayerPrefs.SetFloat("volumenAudio", sliderValue); // Guarda el volumen en PlayerPrefs
        AudioListener.volume = sliderValue; // Establece el volumen del AudioListener
    }
}
