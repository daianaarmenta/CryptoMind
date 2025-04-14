using UnityEngine;
using TMPro;

public class PuntajeEnemigo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Start()
    {
        if (textMesh == null)
        {
            Debug.LogError("❌ No se asignó el TextMeshProUGUI.");
            return;
        }

        ActualizarTexto(); // Mostrar desde el principio
    }

    private void Update()
    {
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        int puntos = GameManager.Instance.Puntaje; // ✅ Línea corregida
        textMesh.text = puntos.ToString("0");
    }
}
