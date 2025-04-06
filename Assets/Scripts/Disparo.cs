using UnityEngine;
/*Autora: Daiana Andrea Armenta Maya
 * Fecha : 05/04/2025
 * Descripción: Clase que gestiona el disparo de balas en el juego.
 * Controla la instanciación de las balas y su posición inicial.
 */
public class Disparo : MonoBehaviour
{
    public GameObject balaPrefab;  // Prefab de la bala
    public Transform puntoDeDisparo; // Donde se disparará la bala (por ejemplo, desde la pistola del personaje)

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
        }
        
    }

    private void Disparar()
    { 
        Instantiate(balaPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation); // Instancia la bala en el punto de disparo
    }
}


