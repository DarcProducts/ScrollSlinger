using UnityEngine;
using UnityEngine.Events;

public abstract class Potion : MonoBehaviour
{
    public LayerMask affectedLayers;
    public LayerMask unaffectedLayers;
    public Transform startingTransform;
    public UnityEvent OnHitAffectedLayer;
    public UnityEvent OnHitUnaffectedLayer;
    public UnityEvent OnPotionReset;
    public IPotion potionType;

    void OnCollisionEnter(Collision collision) => potionType.ActivatePotion(collision.gameObject);
}