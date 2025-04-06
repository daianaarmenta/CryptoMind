using UnityEngine;
/*Daiana Andrea Armenta Maya A01751408
 * Fecha : 05/04/2025
 * Descripción: Clase que gestiona el tiempo de vida de un objeto en Unity.
 * Destruye el objeto después de un tiempo específico.
 */
public class TiempoVida : MonoBehaviour
{
    [SerializeField] private float tiempoVida;

    private void Start()
    {
        Destroy(gameObject, tiempoVida); 
    }
}
