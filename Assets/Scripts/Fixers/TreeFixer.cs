using UnityEngine;

public class TreeFixer : MonoBehaviour
{
    [SerializeField] LayerMask removeOnLayers;
    [SerializeField] float checkRadius;
    [SerializeField] bool turnOffInstead = true;

    public void CheckIfNearHouse()
    {
        if (Physics.SphereCast(transform.position + Vector3.up * 2, checkRadius, Vector3.down * 2, out RaycastHit _, 10, removeOnLayers))
        {
            if (turnOffInstead)
                gameObject.SetActive(false);
            else
                Destroy(gameObject);
        }
    }
}