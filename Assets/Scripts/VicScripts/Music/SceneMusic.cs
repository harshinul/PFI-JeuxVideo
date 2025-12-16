using UnityEngine;
using UnityEngine.UI;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip sceneMusic;
    [SerializeField] private AudioClip tenseMusic;
    [SerializeField] private AudioClip casualCopMusic;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private float volume;
    [SerializeField] private float tenseVolume;

    bool doOnceMusicPistol = true;
    bool doOnceMusicRifle = true;

    void Start()
    {
        if (ScriptMusic.Instance != null)
        {
            ScriptMusic.Instance.PlayMusic(volume, sceneMusic, fadeDuration);
        }
    }

    private void Update()
    {
        changeWhenWanted();
    }

    void changeWhenWanted()
    {
        if (ScriptMusic.Instance == null || GameManager.Instance == null) return;

        if (GameManager.Instance.wastedCount >= 150)
        {
            if (doOnceMusicRifle)
            {
                ScriptMusic.Instance.PlayMusic(tenseVolume, tenseMusic, fadeDuration);
                doOnceMusicRifle = false;
            }
        }
        else if (GameManager.Instance.wastedCount >= 75)
        {
            if (doOnceMusicPistol)
            {
                ScriptMusic.Instance.PlayMusic(volume, casualCopMusic, fadeDuration);
                doOnceMusicPistol = false;
            }
        }
    }
}
