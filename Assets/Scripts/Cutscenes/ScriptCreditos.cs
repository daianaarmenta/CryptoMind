using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptCreditos : MonoBehaviour
{
    public float scrollSpeed = 20f;
    public float limiteFinalY = 1000f; // Ajusta este valor seg�n el tama�o de los cr�ditos
    public string nombreEscenaMenu = "MenuJuego"; 

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
