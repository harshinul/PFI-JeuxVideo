using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ScriptMusic : MonoBehaviour
{
    public static ScriptMusic Instance;

    [SerializeField] private AudioClip music;
    [SerializeField] private Image background;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private float volume = 1f;

    private AudioSource audioSource;
    private Coroutine fadeRoutine;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = volume;

        if (music != null && !audioSource.isPlaying)
        {
            audioSource.clip = music;
            audioSource.Play();
        }
    }

    public void Start()
    {
        StartCoroutine(FadeTo(0));
    }

    public void SetBackground(Image newBackground)
    {
        background = newBackground;
    }

    public void StartFade()
    {
        if (background == null) return;

        background.gameObject.SetActive(true);

        var c = background.color;
        background.color = new Color(c.r, c.g, c.b, 1f);

        StartCoroutine(FadeTo(0));
    }

    IEnumerator FadeTo(float targetAlpha)
    {
        yield return new WaitForSeconds(0.5f);
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
        background.gameObject.SetActive(false);
    }

    public void PlayMusic(float volume, AudioClip newClip, float fadeDuration = -1f)
    {
        if (newClip == null) return;

        if (audioSource.isPlaying && audioSource.clip == newClip) return;

        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(Crossfade(volume, newClip, fadeDuration));
    }

    private IEnumerator Crossfade(float volume, AudioClip newClip, float duration)
    {
        float half = Mathf.Max(0.01f, duration * 0.5f);

        float startVol = audioSource.volume;
        if (audioSource.isPlaying)
        {
            float t = 0f;
            while (t < half)
            {
                t += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVol, 0f, t / half);
                yield return null;
            }
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
        audioSource.volume = volume;

        float t2 = 0f;
        while (t2 < half)
        {
            t2 += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, volume, t2 / half);
            yield return null;
        }

        audioSource.volume = volume;
        fadeRoutine = null;
    }


}