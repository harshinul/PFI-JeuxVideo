using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInNOut : MonoBehaviour
{
    [SerializeField] public Image background;
    [SerializeField] private float fadeDuration = 1.5f;
    bool doOnce = true;
    void Start()
    {
        StartCoroutine(FadeTo(0f));
    }

    IEnumerator FadeTo(float targetAlpha)
    {
        yield return new WaitForSeconds(0.5f);
        Color c = background.color;
        float startAlpha = c.a;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            background.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }

        background.color = new Color(c.r, c.g, c.b, targetAlpha);
        if (doOnce)
        {
            background.gameObject.SetActive(false);
            doOnce = false;
        }
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeTo(1f));
    }
}
