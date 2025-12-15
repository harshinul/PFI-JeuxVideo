using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OnDeath : MonoBehaviour
{
    [SerializeField] float fadeDuration;
    [SerializeField] GameObject wasted;
    [SerializeField] float wobbleSpeed = 2f;
    [SerializeField] float wobbleAmount = 5f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] Image background;
    [SerializeField] Camera deathCam;

    AudioSource mainAudioSource;

    public static OnDeath Instance;

    private Camera cameraMain;
    private Volume volume;

    private Quaternion originalRot;
    void Awake()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        volume = deathCam.gameObject.GetComponent<Volume>();
        originalRot = deathCam.transform.rotation;
        deathCam.gameObject.SetActive(false);
        mainAudioSource = FindFirstObjectByType<AudioSource>();
    }

    public void clickDeath()
    {
        cameraMain.gameObject.SetActive(false);
        deathCam.gameObject.SetActive(true);
         StartCoroutine(DeathIn());
        StartCoroutine(WastedImage());
        StartCoroutine(SlowMo());
        startDeathMusic();
        Wobble();
    }

    void Wobble()
    {
        float t = Time.unscaledTime;

        float x = (Mathf.PerlinNoise(t * wobbleSpeed, 0.0f) - 0.5f) * 2f * wobbleAmount;

        Quaternion targetRot = originalRot * Quaternion.Euler(x, 0f, 15f);

        deathCam.transform.rotation = Quaternion.Lerp(deathCam.transform.rotation, targetRot, Time.deltaTime * 2f);
    }

    void startDeathMusic()
    {
        mainAudioSource.Stop();
        SFXManager.Instance.PlaySFX(deathSound, transform, 1);
    }

    IEnumerator DeathIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            volume.weight = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        volume.weight = 1f;
    }

    IEnumerator WastedImage()
    {
        yield return new WaitForSeconds(0.45f);

        wasted.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);

        background.gameObject.SetActive(true);
        Time.timeScale = 1;
        FindFirstObjectByType<FadeInNOut>().StartFadeIn();

        yield return new WaitForSecondsRealtime(2f);

        FindFirstObjectByType<SceneLoader>().LoadMainMenu();
    }

    IEnumerator SlowMo()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(1f, 0.1f, t / fadeDuration);
            yield return null;
        }
    }
}
