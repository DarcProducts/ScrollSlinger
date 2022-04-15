using UnityEngine;

public class MultiObjectPool : MonoBehaviour
{
    [SerializeField] ObjectPool[] objectPools;

    public GameObject GetRandomObject() => objectPools[Random.Range(0, objectPools.Length)].GetObject();
}
