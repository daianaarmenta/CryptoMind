using UnityEngine;

/*
Autor: María Fernanda Pineda Pat
Controla el movimiento oscilatorio de un enemigo de un lado a otro, de forma horizontal.
*/
public class MovimientoEnemigo : MonoBehaviour
{
    public float velocidad = 2f;          // Qué tan rápido se mueve
    public float distancia = 3f;          // Qué tan lejos se mueve desde su punto inicial

    private Vector3 puntoInicial;

    // Guarda la posición original al iniciar para usarla como punto de referencia.
    void Start()
    {
        puntoInicial = transform.position; // Guarda su posición original
    }

    // Actualiza la posición del objeto cada frame con un patrón senoidal.
    void Update()
    {
        float desplazamiento = Mathf.Sin(Time.time * velocidad) * distancia;
        transform.position = puntoInicial + new Vector3(desplazamiento, 0f, 0f);
    }
}
