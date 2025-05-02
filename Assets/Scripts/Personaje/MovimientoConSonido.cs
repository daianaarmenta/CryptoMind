using UnityEngine;
/* Autora: Daiana Andrea Armenta Maya
    Clase que gestiona los sonidos de movimiento del personaje.
 */
public class SonidosMovimiento : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoSalto;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSalto()
    {
        if (sonidoSalto != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoSalto);
        }
        else
        {
            Debug.LogWarning("Falta AudioClip o AudioSource para salto");
        }
    }
}


