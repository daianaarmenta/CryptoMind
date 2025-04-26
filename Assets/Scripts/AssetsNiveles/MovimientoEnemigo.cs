using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public float velocidad = 2f;          // Qué tan rápido se mueve
    public float distancia = 3f;          // Qué tan lejos se mueve desde su punto inicial

    private Vector3 puntoInicial;

    void Start()
    {
        puntoInicial = transform.position; // Guarda su posición original
    }

    void Update()
    {
        float desplazamiento = Mathf.Sin(Time.time * velocidad) * distancia;
        transform.position = puntoInicial + new Vector3(desplazamiento, 0f, 0f);
    }
}
