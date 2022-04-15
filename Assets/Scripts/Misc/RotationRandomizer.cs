using UnityEngine;

public class RotationRandomizer : MonoBehaviour
{
    [SerializeField] Vector3 minRotationVector;
    [SerializeField] Vector3 maxRotationVector;
    void OnEnable()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(
                Random.Range(minRotationVector.x, maxRotationVector.x),
                Random.Range(minRotationVector.y, maxRotationVector.y), 
                Random.Range(minRotationVector.z, maxRotationVector.z)));
    }
}
