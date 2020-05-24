using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("Text Labels")]
    public Text scoreText;
    public Text highScoreText;

    [Header("Variables")]
    public float scoreCount;
    public float highScoreCount;
    public float pointPerSecond;
    public bool scoreIncreasing;

    public static float startingPointsPerSeconds = 5f;

    public bool shouldDouble;

    private static ScoreManager _instance;
    public static ScoreManager instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointPerSecond * Time.deltaTime * 0.3f;

        }

        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
    }

    public void AddScore(int pointsToAdd)
    {
        if (shouldDouble)
        {
            pointsToAdd *= 2;
        }
        scoreCount += pointsToAdd;
    }
}
