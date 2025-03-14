using UnityEngine;
using System.Collections;

public class SaludPersonaje : MonoBehaviour
{
    public int vidas = 3;
    public int vidasMaximas = 3;
    public float tiempoRegeneracion = 5f; // Tiempo en segundos para regenerar una vida

    public static SaludPersonaje instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PerderVida()
    {
        if (vidas > 0)
        {
            vidas--;
            VidasHUD.instance.ActualizarVidas();
            Debug.Log("Vida perdida. Vidas restantes: " + vidas);

            StartCoroutine(RegenerarVida());
        }
        else
        {
            Debug.Log("No quedan más vidas.");
        }
    }

    private IEnumerator RegenerarVida()
    {
        while (vidas < vidasMaximas)
        {
            yield return new WaitForSeconds(tiempoRegeneracion);
            vidas++;
            VidasHUD.instance.ActualizarVidas();
            Debug.Log("Vida regenerada. Vidas actuales: " + vidas);
        }
    }
}
