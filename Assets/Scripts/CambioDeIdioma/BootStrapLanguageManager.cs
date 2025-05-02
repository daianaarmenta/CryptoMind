using UnityEngine;
/* Autor: Emiliano Plata Cardona
 * Descripción: Clase que gestiona la instancia del LanguageManager al inicio del juego.
 * Si no existe una instancia, crea una nueva.
 */
public class BootstrapLanguageManager : MonoBehaviour
{
    [SerializeField] private GameObject LanguageManager;

    private void Awake()
    {
        if (FindFirstObjectByType<LanguageManager>() == null)
        {
            Debug.Log("Instanciando LanguageManager desde Bootstrapper.");
            Instantiate(LanguageManager);
        }
        else
        {
            Debug.Log("LanguageManager ya está presente.");
        }
    }
}