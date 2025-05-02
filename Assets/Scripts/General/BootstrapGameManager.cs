using UnityEngine;

/*
Autor: María Fernanda Pineda Pat
Este script se encarga de instanciar el prefab del GameManager si aún no existe.
Se recomienda colocarlo en la primera escena del juego para asegurar su existencia global.
*/
public class BootstrapGameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerPrefab;

    //  Se ejecuta al iniciar el objeto. Verifica si ya existe el GameManager; si no, lo instancia.
    private void Awake()
    {
        if (GameManagerBase.Instance == null)
        {
            //Debug.Log(" Instanciando GameManager desde Bootstrapper.");
            Instantiate(gameManagerPrefab);
        }
        else
        {
            //Debug.Log(" GameManager ya está presente.");
        }
    }
}
