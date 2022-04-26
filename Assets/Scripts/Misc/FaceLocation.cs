using UnityEngine;

public class FaceLocation : MonoBehaviour
{
    [SerializeField] Vector3 targetLocation;
    [SerializeField] bool onlyAtStart;
    [SerializeField] bool invert;

    void Start()
    {
        if (!onlyAtStart) return;
        if (!invert)
            transform.localRotation = Quaternion.LookRotation(targetLocation - transform.localPosition, Vector3.up);
        else
            transform.localRotation = Quaternion.LookRotation((targetLocation - transform.localPosition) * -1, Vector3.up);
    }

    void FixedUpdate()
    {
        if (onlyAtStart) return;
        if (!invert)
            transform.localRotation = Quaternion.LookRotation(targetLocation - transform.localPosition, Vector3.up);
        else
            transform.localRotation = Quaternion.LookRotation((targetLocation - transform.localPosition) * -1, Vector3.up);
    }
} 