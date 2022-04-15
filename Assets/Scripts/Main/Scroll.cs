using UnityEngine;
using UnityEngine.Events;

public class Scroll : MonoBehaviour
{
    [SerializeField] Transform startingTransform;
    [SerializeField] float maxDistanceFromMailbox;
    [SerializeField] LayerMask mailboxLayer;
    [SerializeField] LayerMask resetLayers;
    [SerializeField] UnityEvent OnScrollReset;
    [SerializeField] GameEvent OnScrollDelivered;
    [SerializeField] UnityEvent<GameObject> OnScrollMiss;

    void OnCollisionEnter(Collision collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject, resetLayers))
        {
            if (CheckIfCloseToMailBox())
                OnScrollDelivered.Invoke(gameObject);
            OnScrollMiss?.Invoke(gameObject);
            ResetScrollImmediate();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!Utils.IsInLayerMask(other.gameObject, mailboxLayer)) return;
        if (other.TryGetComponent<ITakeScroll>(out ITakeScroll takesScroll))
        {
            if (takesScroll.ScrollDelivered(true))
                ResetScrollImmediate();
        }
    }

    void ResetScrollImmediate()
    {
        transform.SetPositionAndRotation(startingTransform.position, startingTransform.rotation);
        OnScrollReset?.Invoke();
    }

    public bool CheckIfCloseToMailBox()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, maxDistanceFromMailbox, mailboxLayer);
        foreach (var h in hits)
            if (h.transform.TryGetComponent<ITakeScroll>(out ITakeScroll takesScroll))
                if (takesScroll.ScrollDelivered(false))
                    return true;
        return false;
    }
}