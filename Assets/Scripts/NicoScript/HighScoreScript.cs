using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour
{

    public static HighScoreScript Instance;

    private const string highScoreKey = "HighScore";
    [SerializeField] TextMeshProUGUI highScoreText;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ShowHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHighScore()
    {
        int score = PlayerPrefs.GetInt(highScoreKey, 0);
        highScoreText.text = "High Score: " + score;
    }

    public void SetHighScore(int newScore)
    {
        int currentHighScore = PlayerPrefs.GetInt(highScoreKey, 0);
        if (newScore > currentHighScore)
        {
            PlayerPrefs.SetInt(highScoreKey, newScore);
            PlayerPrefs.Save();
        }
    }
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey(highScoreKey);
        PlayerPrefs.Save();
        Debug.Log(highScoreKey);
        ShowHighScore();
    }
}
