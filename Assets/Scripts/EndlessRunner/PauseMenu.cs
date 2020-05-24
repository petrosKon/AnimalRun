using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Levels")]
    public string mainMenuLevel;
    public string leaderboardLevel;
    public string characterSelectLevel;

    [Header("Pause Menu")]
    public GameObject thePauseMenu;

    [Header("Crossfade Animator")]
    public Animator crossfadeAnimator;

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        thePauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        GameManager.instance.Reset();

        thePauseMenu.SetActive(false);

        Time.timeScale = 1f;
    }

    public void MainMenuLevel()
    {
        Time.timeScale = 1f;

        thePauseMenu.SetActive(false);

        SceneManager.LoadScene(mainMenuLevel);

    }

    public void LeaderboardLevel()
    {
        Time.timeScale = 1f;

        thePauseMenu.SetActive(false);

        SceneManager.LoadScene(leaderboardLevel);

    } 
    
    public void CharacterSelectLevel()
    {
        Time.timeScale = 1f;

        thePauseMenu.SetActive(false);

        SceneManager.LoadScene(characterSelectLevel);

    }

}
