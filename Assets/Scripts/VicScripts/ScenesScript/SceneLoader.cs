using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image background;

    private void Start()
    {
        if(ScriptMusic.Instance != null)
        {
            ScriptMusic.Instance.SetBackground(background);
            ScriptMusic.Instance.StartFade();
        }
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MapTestNico");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuPrincipal1");
    }
}
