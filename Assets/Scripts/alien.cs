using UnityEngine;

public class alien : MonoBehaviour
{
    [SerializeField] private int cantidadCheckpoint;
    [SerializeField] private float checkpointTerminado;

    void Start()
    {
        cantidadCheckpoint = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        
    }
}
