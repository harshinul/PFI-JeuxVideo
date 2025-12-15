using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInNOut : MonoBehaviour
{
    [SerializeField] public Image background;
    [SerializeField] private float fadeDuration = 1.5f;

    void Start()
    {
        StartCoroutine(FadeTo(0f));
    }

    IEnumerator FadeTo(float targetAlpha)
    {
        Color c = background.color;
        float startAlpha = c.a;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            background.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }

        background.color = new Color(c.r, c.g, c.b, targetAlpha);

        if (targetAlpha <= 0f)
            background.gameObject.SetActive(false);
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeTo(1f));
    }
}
