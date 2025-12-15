using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image background;

    private void Start()
    {
        ScriptMusic.Instance.SetBackground(background);
        ScriptMusic.Instance.StartFade();
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("RealMapHarsh");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
