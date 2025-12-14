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

    AudioSource mainAudioSource;

    private Camera cameraDeath;
    private Camera cameraMain;
    private Volume volume;

    private Quaternion originalRot;

    bool isDead = false;
    void Awake()
    {
        cameraDeath = GameObject.FindGameObjectWithTag("DeathCam").GetComponent<Camera>();
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        volume = cameraDeath.gameObject.GetComponent<Volume>();
        originalRot = cameraDeath.transform.rotation;
        cameraDeath.gameObject.SetActive(false);
        mainAudioSource = FindFirstObjectByType<AudioSource>();
    }

    private void Update()
    {
        //TEST
        clickDeath();
        if (isDead)
            Wobble();
    }

    public void clickDeath()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            cameraMain.gameObject.SetActive(false);
            cameraDeath.gameObject.SetActive(true);
            StartCoroutine(DeathIn());
            StartCoroutine(WastedImage());
            StartCoroutine(SlowMo());
            startDeathMusic();
            isDead = true;
        }
    }

    void Wobble()
    {
        float t = Time.unscaledTime;

        float x = (Mathf.PerlinNoise(t * wobbleSpeed, 0.0f) - 0.5f) * 2f * wobbleAmount;

        Quaternion targetRot = originalRot * Quaternion.Euler(x, 0f, 15f);

        cameraDeath.transform.rotation = Quaternion.Lerp(cameraDeath.transform.rotation, targetRot, Time.deltaTime * 2f);
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
