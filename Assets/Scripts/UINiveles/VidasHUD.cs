using UnityEngine;
using TMPro;

/*
Autor: María Fernanda Pineda Pat
Controla la visualizacion de las vidas del jugador en el HUD
Utiliza Singleton para acceso globar y permite acutalizar desde SaludPersonaje o desde GameManager.
*/

public class VidasHUD : MonoBehaviour
{
    public static VidasHUD instance;

    [SerializeField] private TextMeshProUGUI vidasTexto;

    
    // Asigna la instancia Singleton en el momento de la creación. Si hay una, elimina el duplicado. 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Llama a la actualización del HUD al iniciar.
    void Start()
    {
        ActualizarVidas();
    }

    // Actualiza el texto del HUD con el número actual de vidas del jugador, obteniendo desde la instancia de SaludPersonaje. 
    public void ActualizarVidas()
    {
        if (SaludPersonaje.instance == null)
        {
            //Debug.LogWarning(" No se puede actualizar HUD, SaludPersonaje no está listo.");
            return;
        }

        int vidas = SaludPersonaje.instance.vidas;

        if (vidasTexto != null)
        {
            vidasTexto.text = $" {vidas}";
            //Debug.Log(" Texto de vidas actualizado: " + vidas);
        }
        else
        {
            //Debug.LogWarning(" El campo de texto no está asignado.");
        }
    }

    // Reinicia las vidas al valor máximo y actualiza el HUD.
    public void ReiniciarVidas()
    {
        if (SaludPersonaje.instance != null)
        {
            SaludPersonaje.instance.vidas = SaludPersonaje.instance.vidasMaximas;
            ActualizarVidas();
        }
    }

    // Actualiza el número de vidas en el HUD utilizando el valor almacenado en GameManager. 
    // Este método es útil cuando se compra una vida desde la tienda. 
    public void SetVidasDesdeGameManager()
    {
        if (vidasTexto != null)
        {
            int vidas = GameManagerBase.Instance.VidasGuardadas;
            vidasTexto.text = $" {vidas}";
            //Debug.Log(" Texto de vidas actualizado desde GameManager: " + vidas);
        }
    }
}
