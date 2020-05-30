using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public string[] enemyNames;
    private bool doublePoints;
    private bool safeMode;

    [HideInInspector]
    public bool powerUpActive;
      
    private float powerUpLengthCounter;

    private PlatformGenerator thePlatformGenerator;

    public float normalPointsPerSecond;
    private float cactusRate;

    private ObjectDestroyer[] enemyList;

    private static PowerUpManager _instance;
    public static PowerUpManager instance { get { return _instance; } }

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
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpActive)
        {
            powerUpLengthCounter -= Time.deltaTime;

            if (GameManager.instance.powerUpReset)
            {
                powerUpLengthCounter = 0;
                GameManager.instance.powerUpReset = false;
            }

            if (doublePoints)
            {
                if(ScoreManager.instance.pointPerSecond >= 10)
                {
                    ScoreManager.instance.pointPerSecond = 10;
                }
                else
                {
                    ScoreManager.instance.pointPerSecond = normalPointsPerSecond * 2f;

                }
                ScoreManager.instance.shouldDouble = true;
            }

            if (safeMode)
            {
                thePlatformGenerator.randomEnemyThreshold = 0f;
            }

            if(powerUpLengthCounter <= 0)
            {
                ScoreManager.instance.pointPerSecond = normalPointsPerSecond;
                thePlatformGenerator.randomEnemyThreshold = cactusRate;
                ScoreManager.instance.shouldDouble = false;

                powerUpActive = false;
            }
        }
    }

    public void ActivatePowerUp(bool points, bool safe, float time)
    {
        doublePoints = points;
        safeMode = safe;
        powerUpLengthCounter = time;

        normalPointsPerSecond = ScoreManager.startingPointsPerSeconds;
        cactusRate = thePlatformGenerator.randomEnemyThreshold;

        if (safeMode)
        {
            enemyList = FindObjectsOfType<ObjectDestroyer>();

            for (int i = 0; i < enemyList.Length; i++)
            {
                if (enemyList[i].name.Contains("Cactus") || enemyList[i].name.Contains("Spikes") || enemyList[i].name.Contains("WoodTrap") || enemyList[i].name.Contains("EnemyBird"))
                {
                    enemyList[i].gameObject.SetActive(false);
                }
            }
        }

        powerUpActive = true;
    }
}
