using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    /*[Header("Platform")]
    public GameObject[] thePlatforms;*/

    [Header("Generation Point")]
    public Transform generationPoint;

    //set min = 0 & max = 0 if you want the platform to be touching each other
    [Header("Distance Between Platforms")]
    public float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    [Header("ObjectPools")]
    public ObjectPooler[] theObjectPools;

    [Header("Height Variables")]
    public Transform maxHeightPoint;
    public float maxHeightChange;

    private float[] platformWidths;
    private int platformSelector;
    private float minHeight;
    private float maxHeight;
    private float heightChange;
    private PickupGenerator thePickUpGenerator;
    public float randomPickUpsThreshold;

    public float randomEnemyThreshold;
    public ObjectPooler[] enemiesPool;

    public float powerUpHeight;
    public ObjectPooler powerUpPool;
    public float powerUpThreshold;

    // Start is called before the first frame update
    void Start()
    {
        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        thePickUpGenerator = FindObjectOfType<PickupGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        //generation point goes along with camera position
        if (transform.position.x < generationPoint.position.x)
        {
            //comment to create a continuous platform
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            heightChange = Mathf.Clamp(transform.position.y + Random.Range(-maxHeight, maxHeight), minHeight, maxHeight);

            //prevent from spawning two power ups simultaneously
            if (!PowerUpManager.instance.powerUpActive)
            {
                if (Random.Range(0f, 100f) < powerUpThreshold)
                {
                    GameObject newPowerUp = powerUpPool.GetPooledObject();

                    newPowerUp.transform.position = transform.position + new Vector3(distanceBetween / 2, Random.Range(powerUpHeight / 2, powerUpHeight), 0f);

                    newPowerUp.SetActive(true);

                }
            }
            else
            {
                if (FindObjectsOfType<PowerUps>() != null)
                {
                    foreach (PowerUps powerUp in FindObjectsOfType<PowerUps>())
                    {
                        powerUp.gameObject.SetActive(false);

                    }
                }
            }

            //we change the position of the generation point
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween,
                heightChange,
                transform.position.z);

            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
            //Debug.Log(newPlatform.transform.position);

            if (Random.Range(0f, 100f) < randomEnemyThreshold)
            {
                GameObject newEnemy = enemiesPool[Random.Range(0, enemiesPool.Length)].GetPooledObject();

                float enemyXPos = Random.Range(-platformWidths[platformSelector] / 3, platformWidths[platformSelector] / 3);

                Vector3 enemyPos = new Vector3(enemyXPos, 0.5f, 0f);
                if (Physics.Raycast(new Vector3(enemyXPos, 9999f, 0f), Vector3.down, out RaycastHit hit, Mathf.Infinity))
                {
                    enemyPos = new Vector3(enemyXPos, hit.point.y + 0.5f, 0f);

                }

                newEnemy.transform.position = transform.position + enemyPos;
                newEnemy.transform.rotation = transform.rotation;
                newEnemy.SetActive(true);

                //prevents pickups from spawning inside enemies!!!
                if (Random.Range(0f, 100f) < randomPickUpsThreshold)
                {
                    List<GameObject> pickups = thePickUpGenerator.SpawnPickups(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));

                    foreach (GameObject pickup in pickups)
                    {
                        if (pickup.GetComponent<BoxCollider2D>().bounds.Intersects(newEnemy.GetComponent<BoxCollider2D>().bounds))
                        {
                            pickup.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                if (Random.Range(0f, 100f) < randomPickUpsThreshold)
                {
                    thePickUpGenerator.SpawnPickups(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
                }
            }

        transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2),
        transform.position.y,
        transform.position.z);

            // Instantiate(thePlatforms[platformSelector],transform.position,transform.rotation);
        }
    }
}
