using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedActivate : MonoBehaviour
{
    [SerializeField] List<GameObject> objectsToActivate;
    [SerializeField] float activateDelay;

    void OnEnable()
    {
        foreach (var o in objectsToActivate)
            o.SetActive(false);
        StartCoroutine(ActivateObject());
    }

    IEnumerator ActivateObject()
    {
        SetObjectsActive(false);
        foreach (var g in objectsToActivate)
        {
            yield return new WaitForSeconds(activateDelay);
            g.SetActive(true);
        }
    }

    void SetObjectsActive(bool active)
    {
        foreach (var g in objectsToActivate)
            g.SetActive(active);
    }
}