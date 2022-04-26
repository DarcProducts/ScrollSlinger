using System.Collections;
using UnityEngine;

public class LocationRandomizer : MonoBehaviour
{
    [SerializeField] Vector3 minPosition;
    [SerializeField] Vector3 maxPosition;
    [SerializeField] Vector2 xPositionRemove;
    [SerializeField] LayerMask turnOffOnLayers;
    [SerializeField] float checkTime;
    [SerializeField] float checkRadius;

    void OnEnable() => TrySetLocation();

    void TrySetLocation()
    {
        Vector3 tryLoc = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z));
        transform.localPosition = tryLoc;
        StartCoroutine(CheckSurroundings());
    }

    IEnumerator CheckSurroundings()
    {
        yield return new WaitForSeconds(checkTime);
        if (Physics.CheckSphere(transform.localPosition, checkRadius, turnOffOnLayers))
            gameObject.SetActive(false);
        if (transform.localPosition.x > xPositionRemove.x && transform.localPosition.x < xPositionRemove.y)
            gameObject.SetActive(false);
    }
}