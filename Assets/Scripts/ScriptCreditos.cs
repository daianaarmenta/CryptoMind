using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptCreditos : MonoBehaviour
{
    public float scrollSpeed = 20f;
    public float limiteFinalY = 1000f; // Ajusta este valor según el tamaño de tus créditos
    public string nombreEscenaMenu = "MenuJuego"; // Nombre de tu escena de menú

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        if (rectTransform.anchoredPosition.y >= limiteFinalY)
        {
            SceneManager.LoadScene(nombreEscenaMenu);
        }
    }
}
