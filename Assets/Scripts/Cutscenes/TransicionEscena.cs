using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

/*
Autor: María Fernanda Pineda Pat
Controla transiciones suaves entre escenas usando un panel negro con efecto de fade in/out.
*/
public class TransicionEscena : MonoBehaviour
{
    [SerializeField] private Image panelNegro;
    [SerializeField] private float duracionTransicion = 1.5f;

    //  Al iniciar, activa el panel negro y comienza el efecto de aparición (fade in).
    private void Start()
    {
        if (panelNegro != null)
        {
            panelNegro.gameObject.SetActive(true);
            panelNegro.color = new Color(0, 0, 0, 1); // empieza completamente negro
            StartCoroutine(FadeIn());
        }
    }

    // Carga la siguiente escena en el Build Index con un efecto de fundido.
    public void IrASiguienteEscena()
    {
        int siguiente = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(FadeOut(() => SceneManager.LoadScene(siguiente)));
    }

    // Carga una escena específica por su nombre con una transición suave.
    public void IrAEscena(string nombreEscena)
    {
        StartCoroutine(FadeOut(() => SceneManager.LoadScene(nombreEscena)));
    }

    // Efecto de aparición: desaparece lentamente el panel negro al iniciar.
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

    // Efecto de desaparición: vuelve a cubrir la pantalla de negro antes de cambiar de escena.
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
