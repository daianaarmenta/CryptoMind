using UnityEngine;
using System.Collections;

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
            float alpha = Mathf.Lerp(from, to, t / fadeDuration);
            SetAlpha(alpha);
            t += Time.deltaTime;
            yield return null;
        }
        SetAlpha(to);
    }

    private void SetAlpha(float alpha)
    {
        if (sr != null)
        {
            Color c = sr.color;
            sr.color = new Color(c.r, c.g, c.b, alpha);
        }
    }
}

