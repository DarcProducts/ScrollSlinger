using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedActivate : MonoBehaviour
{
    [SerializeField] List<GameObject> objectsToActivate;
    [SerializeField] float activateDelay;

    void OnEnable() => StartCoroutine(ActivateObject());

    IEnumerator ActivateObject()
    {
        SetObjectsActive(false);
        foreach (var g in objectsToActivate)
        {
            yield return new WaitForSeconds(activateDelay);
            g.SetActive(true);
            print($"Activated: {g.name}");
        }
    }

    void SetObjectsActive(bool active)
    {
        foreach (var g in objectsToActivate)
            g.SetActive(active);
    }
}