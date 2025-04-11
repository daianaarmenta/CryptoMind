using UnityEngine;
using TMPro;
public class PuntajeEnemigo : MonoBehaviour
{
    private float puntos;
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        //puntos += Time.deltaTime; // Incrementa los puntos con el tiempo
        textMesh.text = puntos.ToString("0"); // Actualiza el texto con los puntos
    }
    public void SumarPuntos(float puntosEntrada){
        puntos += puntosEntrada; // Suma los puntos al total
    }

}
