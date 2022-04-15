using UnityEngine;

public class CreateObjectMulti : MonoBehaviour
{
    [SerializeField] MultiObjectPool objectPools;

    public void CreateNewObject(GameObject objLoc)
    {
        GameObject o = objectPools.GetRandomObject();
        if (o == null) return;
        o.transform.SetPositionAndRotation(objLoc.transform.position, Quaternion.Euler(0, 0, Random.Range(-180f, 180f)));
        o.SetActive(true);
    }
}
