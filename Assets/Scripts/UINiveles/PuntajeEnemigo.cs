using UnityEngine;
using TMPro;

/*
Autor: María Fernanda Pineda Pat
Este script muestra el puntaje actual del jugador en la interfaz de usuario,
actualizándolo únicamente cuando cambia el valor para optimizar el rendimiento.
*/
public class PuntajeEnemigo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    private int puntajeAnterior = -1; // Para detectar cambios

    private void Start()
    {
        if (textMesh == null)
        {
            //Debug.LogError("No se asignó el TextMeshProUGUI en el Inspector.");
            return;
        }

        ActualizarTexto(); // Mostrar el puntaje desde el inicio
    }

    private void Update()
    {
        if (textMesh == null || GameManagerBase.Instance == null) return;

        int puntajeActual = GameManagerBase.Instance.Puntaje;

        if (puntajeActual != puntajeAnterior)
        {
            ActualizarTexto();
            puntajeAnterior = puntajeActual;
        }
    }

    private void ActualizarTexto()
    {
        if (textMesh == null)
        {
            //Debug.LogWarning(" textMesh no asignado en PuntajeEnemigo.");
            return;
        }

        int puntos = GameManagerBase.Instance.Puntaje;
        textMesh.text = puntos.ToString("0");
    }
}
