using UnityEngine;

/*
Autor: María Fernanda Pineda Pat
Clase generica para implementar el patron Singleton en Unity.
*/
public class SingletonLoader<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    [SerializeField] private bool persistBetweenScenes = true;

    // Se ejecuta al despertar el objeto. Aplica el patrón Singleton y asegura persistencia si se desea.
    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            //Debug.LogWarning(" Duplicado detectado y destruido: " + typeof(T));
            Destroy(gameObject);
            return;
        }

        Instance = this as T;

        if (persistBetweenScenes)
        {
            DontDestroyOnLoad(gameObject);
        }
        //Debug.Log(" SingletonLoader activo: " + typeof(T));
    }
}
