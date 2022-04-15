using UnityEngine;

public class XRRigHelper : MonoBehaviour
{
    [SerializeField] GameObject horse;
    [SerializeField] GameObject body;

    void LateUpdate() => body.transform.position = horse.transform.position;
}
