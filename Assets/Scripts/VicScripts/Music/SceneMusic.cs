using UnityEngine;
using UnityEngine.UI;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip sceneMusic;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private float volume;

    void Start()
    {
        if (ScriptMusic.Instance != null)
        {
            ScriptMusic.Instance.PlayMusic(volume,sceneMusic, fadeDuration);
        }
    }
}
