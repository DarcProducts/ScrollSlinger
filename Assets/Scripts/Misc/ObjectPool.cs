using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject objectToPool;
    readonly List<GameObject> objectPool = new List<GameObject>();
    
    public GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            for (int i = 0; i < objectPool.Count; i++)
                if (!objectPool[i].activeSelf)
                    return objectPool[i];
        }
        GameObject newObj = Instantiate(objectToPool);
        newObj.transform.SetParent(transform);
        objectPool.Add(newObj);
        newObj.SetActive(false);
        return newObj;
    }
}
