using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Platform Generator")]
    public Transform platformGenerator;

    private Vector3 platformStartPoint;
    private Vector3 playerStartPoint;
    private ObjectDestroyer[] objectsList;

    [Header("Menus")]
    public DeathMenu theDeathScreen;
    public PauseMenu thePauseSceen;

    public bool powerUpReset;

    private PlayerController thePlayerController;

    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }

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
        thePlayerController = FindObjectOfType<PlayerController>();
        platformStartPoint = platformGenerator.position;
        playerStartPoint = thePlayerController.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!theDeathScreen.isActiveAndEnabled)
            {
                PauseGame();

            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        thePauseSceen.gameObject.SetActive(true);
    }


    public IEnumerator RestartGame()
    {
        ScoreManager.instance.scoreIncreasing = false;
        yield return new WaitForSeconds(0.1f);    //wait for the animation to end
        thePlayerController.gameObject.SetActive(false);
        theDeathScreen.gameObject.SetActive(true);
 
    }

    public void Reset()
    {
        //we reload the scene in order to prevent errors
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
