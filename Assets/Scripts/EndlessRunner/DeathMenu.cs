using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public string mainMenuLevel;

    [Header("Input Field")]
    public InputField nameImputField;

    public void RestartGame()
    {
        this.gameObject.SetActive(false);
       GameManager.instance.Reset();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenuLevel);
    }

    public void UploadHighScore()
    {
        Highscores.AddNewHighscore(nameImputField.text, (int)FindObjectOfType<ScoreManager>().scoreCount);
    }
}
