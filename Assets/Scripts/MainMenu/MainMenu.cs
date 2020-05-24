using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Levels")]
    public string endlessRunnerLevel;
    public string characterSelectLevel;
    public string leaderboardLevel;
    public string mainMenuLevel;

    [Header("Crossfade animator")]
    public Animator crossfadeAnimator;

    public void PlayGame()
    {
        StartCoroutine(EndlessLevelScreen());
    }

    private IEnumerator EndlessLevelScreen()
    {
        crossfadeAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(endlessRunnerLevel);
    }

    public void CharacterSelect()
    {
        StartCoroutine(CharacterSelectionScreen());
    }

    private IEnumerator CharacterSelectionScreen()
    {
        crossfadeAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(characterSelectLevel);
    }

    public void LeaderboardLevel()
    {
        StartCoroutine(LeaderboardLevelScreen());

    }

    private IEnumerator LeaderboardLevelScreen()
    {
        crossfadeAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(leaderboardLevel);
    }

    public void MainMenuLevel()
    {
        StartCoroutine(MainMenuScreen());

    }

    private IEnumerator MainMenuScreen()
    {
        crossfadeAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(mainMenuLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
