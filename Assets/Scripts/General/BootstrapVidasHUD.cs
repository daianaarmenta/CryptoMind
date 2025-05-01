using UnityEngine;

/*
Autor: María Fernanda Pineda Pat
Este script se encarga de instanciar el pregaf del HUD de vidas, si aun no existe una instancia 
activa en la escena. Debe de colocarse en un GameObject vacío en la primera escena del juego.
*/
public class BootstrapVidasHUD : MonoBehaviour
{
    [SerializeField] private GameObject vidasHUDPrefab;

    // Al iniciarse, verifica si ya existe un VidasHUD. Si no, lo instancia y lo hace persistente.
    private void Awake()
    {
        // Si ya hay una instancia activa, no creamos otra
        if (VidasHUD.instance != null)
        {
            //Debug.Log("VidasHUD ya existe en la escena.");
            return;
        }

        // Instancia el prefab desde el campo asignado
        if (vidasHUDPrefab != null)
        {
            GameObject hudInstanciado = Instantiate(vidasHUDPrefab);
            DontDestroyOnLoad(hudInstanciado);
            //Debug.Log(" VidasHUD instanciado desde bootstrap.");
        }
        else
        {
            //Debug.LogError(" No se ha asignado el prefab de VidasHUD en el inspector.");
        }
    }
}
    