using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [SerializeField] ObjectPool objectToSpawn;
    [SerializeField] Vector3 offset;
    [SerializeField] bool setRandomRotation = true;
    [SerializeField] bool useDynamicRotation;

    public void CreateNewObject(GameObject objLoc)
    {
        GameObject o = objectToSpawn.GetObject();
        if (o == null) return;
        if (setRandomRotation)
            o.transform.SetPositionAndRotation(objLoc.transform.position + offset, Quaternion.Euler(0, 0, Random.Range(-180f, 180f)));
        else if (!setRandomRotation)
            o.transform.SetPositionAndRotation(objLoc.transform.position + offset, Quaternion.identity);
        else
            o.transform.SetPositionAndRotation(objLoc.transform.position + offset, objLoc.transform.rotation);
        o.SetActive(true);
    }
}