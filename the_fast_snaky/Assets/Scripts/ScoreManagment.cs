using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagment : MonoBehaviour
{
    public static ScoreManagment instance;
    public Text scoreText;
    public Text highscoreText;

    int score = 0;
    int highscore = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = "Score :" + score.ToString();
        highscoreText.text = "Highscore :" + highscore.ToString();
    }

    public void AddPoint()
    {
        score++;
        scoreText.text = "Score:" + score.ToString();
        if (highscore < score)
        PlayerPrefs.SetInt("highscore", score);
    }
}
