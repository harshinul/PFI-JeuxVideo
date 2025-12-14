using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour
{
    PlayerPrefs highScore;
    [SerializeField] TextMeshProUGUI highScoreText;

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
        int score = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + score;
    }

    public void SetHighScore(int score)
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }
}
