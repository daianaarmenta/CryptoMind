using UnityEngine;
using TMPro;

public class PuntajeEnemigo : MonoBehaviour
{
    public static PuntajeEnemigo instance;

    private float puntos;
    [SerializeField] private TextMeshProUGUI textMesh;
    private void Awake()
{
    Debug.Log("Awake de PuntajeEnemigo ejecutado en: " + gameObject.name);

    if (instance == null)
    {
        instance = this;
    }
    else
    {
        Destroy(gameObject);
        Debug.LogWarning("Ya existe una instancia de PuntajeEnemigo.");
    }
}


    private void Start()
{
    //PlayerPrefs.SetFloat("PuntajeEnemigo", 0f); //  borra el valor anterior PARA REINICIAR EL PUNTAJE
    puntos = PlayerPrefs.GetFloat("PuntajeEnemigo", 0f); // CARGA EL VALOR GUARDADO
    textMesh.gameObject.SetActive(true);
}

    private void Update()
{
    if (puntos > 0)
    {
        if (!textMesh.gameObject.activeSelf)
            textMesh.gameObject.SetActive(true);

        textMesh.text = puntos.ToString("0");
    }
}
public void SumarPuntos(float puntosEntrada)
{
    puntos += puntosEntrada;

    if (textMesh != null)
    {
        if (!textMesh.gameObject.activeSelf)
            textMesh.gameObject.SetActive(true);

        textMesh.text = puntos.ToString("0");
    }

    PlayerPrefs.SetFloat("PuntajeEnemigo", puntos); // ✅ se guarda al momento

    Debug.Log("🏆 Puntos actuales: " + puntos);
}




}
