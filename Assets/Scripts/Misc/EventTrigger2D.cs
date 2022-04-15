using UnityEngine;
using UnityEngine.Events;

public class EventTrigger2D : MonoBehaviour
{
    // UnityEvents to Invoke when object Enters the trigger
    [SerializeField] UnityEvent OnEntered;
    // UnityEvents to Invoke when object Exits the trigger
    [SerializeField] UnityEvent OnExited;
    // UnityEvents to Invoke when object Stays in the trigger
    [SerializeField] UnityEvent OnStay;
    // layer of object the trigger is trying to detect
    [SerializeField] LayerMask triggerLayers;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject, triggerLayers))
            OnEntered?.Invoke();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject, triggerLayers))
            OnExited?.Invoke();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject, triggerLayers))
            OnStay?.Invoke();
    }
}
