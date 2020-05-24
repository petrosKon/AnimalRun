using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGenerator : MonoBehaviour
{
    public ObjectPooler[] treatPools;

    public float distanceBtwCoins;

    public List<GameObject> SpawnPickups(Vector3 startPosition)
    {
        int random = Random.Range(0, treatPools.Length);
        int numOfSpawns = Random.Range(0, 3);
        List<GameObject> treats = new List<GameObject>();
        GameObject treat1, treat2, treat3;

        switch (numOfSpawns)
        {
            case 0:
                treat1 = treatPools[random].GetPooledObject();
                treat1.transform.position = startPosition;
                treat1.SetActive(true);
                treats.Add(treat1);
                return treats;
            case 1:
                treat1 = treatPools[random].GetPooledObject();
                treat1.transform.position = startPosition;
                treat1.SetActive(true);
                treats.Add(treat1);

                treat2 = treatPools[random].GetPooledObject();
                treat2.transform.position = new Vector3(startPosition.x - distanceBtwCoins, startPosition.y, startPosition.z);
                treat2.SetActive(true);
                treats.Add(treat2);

                return treats;

            case 2:
                treat1 = treatPools[random].GetPooledObject();
                treat1.transform.position = startPosition;
                treat1.SetActive(true);
                treats.Add(treat1);

                treat2 = treatPools[random].GetPooledObject();
                treat2.transform.position = new Vector3(startPosition.x - distanceBtwCoins, startPosition.y, startPosition.z);
                treat2.SetActive(true);
                treats.Add(treat2);

                treat3 = treatPools[random].GetPooledObject();
                treat3.transform.position = new Vector3(startPosition.x + distanceBtwCoins, startPosition.y, startPosition.z);
                treat3.SetActive(true);
                treats.Add(treat3);
                return treats;
            default:
                return null;
        }
    }
}
