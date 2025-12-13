using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip sceneMusic;
    [SerializeField] private float fadeDuration = 1.5f;

    void Start()
    {
        if (ScriptMusic.Instance != null)
            ScriptMusic.Instance.PlayMusic(sceneMusic, fadeDuration);
    }
}
