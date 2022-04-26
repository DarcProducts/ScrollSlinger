using UnityEngine;

public class XRRigHelper : MonoBehaviour
{
    [SerializeField] GameObject horse;
    [SerializeField] GameObject body;
    [SerializeField] bool fixPositionOverHorse;

    void LateUpdate()
    {
        if (fixPositionOverHorse)
            body.transform.position = horse.transform.position;
    }
}
