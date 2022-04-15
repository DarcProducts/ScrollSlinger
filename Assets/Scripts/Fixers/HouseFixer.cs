using UnityEngine;

public class HouseFixer : MonoBehaviour
{
    void OnEnable()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down, out RaycastHit hitInfo, 100))
        {
            GameObject go = hitInfo.collider.gameObject;
            if (go.CompareTag("SmallSlab"))
                transform.position = go.transform.position + .5f * go.transform.localScale.y * Vector3.up;
        }
    }
}
