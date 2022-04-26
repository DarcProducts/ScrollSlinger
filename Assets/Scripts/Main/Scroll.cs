using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Scroll : MonoBehaviour
{
    [SerializeField] float maxDistanceFromMailbox;
    [SerializeField] LayerMask mailboxLayer;
    [SerializeField] LayerMask resetLayers;
    [SerializeField] GameEvent OnScrollDelivered;
    [SerializeField] GameEvent OnScrollMissed;
    [SerializeField] UnityEvent OnScrollReset;
    [SerializeField] ScrollBag[] scrollBags;
    Rigidbody _rigidbody;

    void Awake() => _rigidbody = GetComponent<Rigidbody>();

    void OnCollisionEnter(Collision collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject, resetLayers))
        {
            if (CheckIfCloseToMailBox())
                OnScrollDelivered.Invoke(gameObject);
            OnScrollMissed.Invoke(gameObject);
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

    public void SetScrollToSocketTransform()
    {
        foreach (var s in scrollBags)
        {
            if (s.CanPlaceItem)
                s.SetItemBackToStart(gameObject);
        }
    }

    void ResetScrollImmediate()
    {
        _rigidbody.useGravity = false;
        SetScrollToSocketTransform();
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


    public void SetPhysicsActive(bool usePhysics)
    {
        if (usePhysics)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        }
        else
        {
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }
    }
}