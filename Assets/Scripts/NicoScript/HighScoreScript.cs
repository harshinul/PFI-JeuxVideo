using UnityEngine;

public class HighScoreScript : MonoBehaviour
{
    PlayerPrefs highScore;
    void Start()
    {
        highScore = new PlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
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
