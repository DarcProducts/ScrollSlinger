using UnityEngine;

public class DummyScroll : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;

    void OnCollisionEnter(Collision collision)
    {
        if (!Utils.IsInLayerMask(collision.gameObject, groundLayer)) return;
        transform.SetParent(collision.transform.parent.transform, true);
    }
}
