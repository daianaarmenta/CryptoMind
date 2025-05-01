using UnityEngine;
using UnityEngine.UI;
/*
Autor: María Fernanda Pineda Pat 
Controla el brollo de la pantalla mediante un panel con transparencia
utilizando un slider de UI. El valor del brillo se guarda en PlayerPrefs
*/
public class LogicaBrillo : MonoBehaviour
{
    public Slider sliderBrillo; // Referencia al slider de brillo
    public float sliderValue;   // Valor del brillo
    public Image panelBrillo;   // Referencia al panel que oscurece o aclara la pantalla

    //Se ejecuta al inicio. Recupera el valor del brillo guardado y lo aplica al panel e interfaz
    void Start()
    {
        // Carga el valor de brillo guardado
        sliderValue = PlayerPrefs.GetFloat("brillo", 0.5f);

        //Asigna el valor al slider visualmente 
        sliderBrillo.value = sliderValue;

        // Aplica el brillo invertido para que la izquierda sea oscuro
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, 1 - sliderValue);
    }

    // Metodo llamado cuando el usuario mueve el slider. Actualiza y guarda el brillo. 
    public void CambiarBrillo(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("brillo", sliderValue); // Guarda el valor
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, 1 - sliderValue);
    }
}
