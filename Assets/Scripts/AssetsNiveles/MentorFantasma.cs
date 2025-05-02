using UnityEngine;
using System.Collections;
/*Autora: Daiana Andrea Armenta Maya 
 * Descripción: Clase que controla la aparición y desaparición de un sprite en Unity.
 * Permite hacer un efecto de desvanecimiento (fade) al aparecer o desaparecer el sprite.
 */
public class MentorFantasma : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private float fadeDuration = 1.5f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        SetAlpha(0f); // Inicia invisible
    }

    public void Aparecer()
    {
        gameObject.SetActive(true);
        StartCoroutine(Fade(0f, 1f));
    }

    public void Desaparecer()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            float alpha = Mathf.Lerp(from, to, t / fadeDuration); // Interpolación lineal entre from y to
            SetAlpha(alpha);
            t += Time.deltaTime;
            yield return null; // Espera un frame
        }
        SetAlpha(to);
    }

    private void SetAlpha(float alpha)
    {
        if (sr != null) // Verifica si el componente SpriteRenderer existe
        {
            Color c = sr.color;
            sr.color = new Color(c.r, c.g, c.b, alpha); // Cambia el valor alpha del color del sprite
        }
    }
}

