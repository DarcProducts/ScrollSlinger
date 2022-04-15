using UnityEngine;

public class LocationRandomizer : MonoBehaviour
{
    [SerializeField] Vector3 minPosition;
    [SerializeField] Vector3 maxPosition;
    [SerializeField] Vector2 xPositionRemove;
    [SerializeField] LayerMask turnOffOnLayers;
    [SerializeField] Vector3 offset;
    [SerializeField] float checkRadius;

    void OnEnable() => FindLocation();

    public void FindLocation()
    {
        Vector3 tryLoc = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z));
        if (turnOffOnLayers.value != -1 && Physics.SphereCast(tryLoc + Vector3.up * 10, checkRadius, Vector3.down, out RaycastHit hitInfo, turnOffOnLayers, 100))
        {
            print($"Turning {gameObject.name} off : Hit : {hitInfo.collider.name}");
            gameObject.SetActive(false);
        } 
        if (transform.position.x > xPositionRemove.x && transform.position.x < xPositionRemove.y)
        {
            print($"Turning {gameObject.name} off : Inside path : {transform.TransformVector(tryLoc)}");
            gameObject.SetActive(false);
        }
        else
            transform.localPosition = tryLoc;
    }
}