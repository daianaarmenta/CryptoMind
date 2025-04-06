using UnityEngine;

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


