using UnityEngine;

public class FaceLocation : MonoBehaviour
{
    [SerializeField] Vector3 targetLocation;
    [SerializeField] bool onlyAtStart;

    void Start()
    {
        if (!onlyAtStart) return;
        transform.localRotation = Quaternion.LookRotation(targetLocation - transform.localPosition, Vector3.up);
    }

    void FixedUpdate()
    {
        if (onlyAtStart) return;
        transform.localRotation = Quaternion.LookRotation(targetLocation - transform.localPosition, Vector3.up);
    }
} 