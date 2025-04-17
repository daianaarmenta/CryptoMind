using UnityEngine;
/*Autora: Daiana Andrea Armenta Maya
 * Fecha : 05/04/2025
 * Descripción: Clase que gestiona el disparo de balas en el juego.
 * Controla la instanciación de las balas y su posición inicial.
 */
public class Disparo : MonoBehaviour
{
    public GameObject balaPrefab;  // Prefab de la bala
    public Transform puntoDeDisparoParado; // Donde se disparará la bala (por ejemplo, desde la pistola del personaje)
    public Transform puntoDeDisparoAgachado; // Donde se disparará la bala si el personaje está agachado

    public AudioClip sonidoDisparo;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (MueveChabelito.estaAgachado)
            {
                Disparar();
            }
            else 
            {
                Disparar();
            }
        }
        
    }

    private void Disparar()
    {
        Transform puntoDeDisparo = MueveChabelito.estaAgachado ? puntoDeDisparoAgachado : puntoDeDisparoParado;
        Instantiate(balaPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation); // Instancia la bala en el punto de disparo
        audioSource.PlayOneShot(sonidoDisparo,1f);

        Bala balaScript = balaPrefab.GetComponent<Bala>();
        if (balaScript != null)
        {
            balaScript.SetDaño(GameManager.Instance.DañoBala); // Asigna el daño a la bala
        }
        else
        {
            Debug.LogError("❌ No se encontró el script Bala en el prefab de la bala.");
        }
    }

}


