using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Pooled Object")]
    public GameObject pooledObject;

    [Header("Amount")]
    public int pooledAmount;

    List<GameObject> pooledObjects;

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();

        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject) as GameObject;   //casting it as a GameObject
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

   public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        GameObject obj = Instantiate(pooledObject) as GameObject;   //casting it as a GameObject
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}
