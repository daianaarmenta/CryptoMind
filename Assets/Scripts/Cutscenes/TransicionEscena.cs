using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TransicionEscena : MonoBehaviour
{
    [SerializeField] private Image panelNegro;
    [SerializeField] private float duracionTransicion = 1.5f;

    private void Start()
    {
        if (panelNegro != null)
        {
            panelNegro.gameObject.SetActive(true);
            panelNegro.color = new Color(0, 0, 0, 1); // empieza completamente negro
            StartCoroutine(FadeIn());
        }
    }

    public void IrASiguienteEscena()
    {
        int siguiente = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(FadeOut(() => SceneManager.LoadScene(siguiente)));
    }

    public void IrAEscena(string nombreEscena)
    {
        StartCoroutine(FadeOut(() => SceneManager.LoadScene(nombreEscena)));
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < duracionTransicion)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / duracionTransicion);
            panelNegro.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        panelNegro.gameObject.SetActive(false); // se oculta cuando ya no se necesita
    }

    private IEnumerator FadeOut(System.Action alTerminar)
    {
        panelNegro.gameObject.SetActive(true);
        float t = 0f;

        while (t < duracionTransicion)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / duracionTransicion);
            panelNegro.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        alTerminar?.Invoke();
    }
}
