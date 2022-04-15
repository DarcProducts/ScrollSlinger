using System.Collections;
using UnityEngine;

public class ExplosivePotion : Potion, IPotion
{
    [SerializeField] float detonationTime;
    [SerializeField] float blastForce;
    [SerializeField] float upwardsModifier;
    [SerializeField] float blastRadius;
    [SerializeField] GameEvent OnPotionExploded;
    bool hasTriggered;


    void OnEnable() => potionType = this;

    void OnDisable() => StopAllCoroutines();

    IEnumerator ExplodePotion()
    {
        yield return new WaitForSeconds(detonationTime);
        Collider[] expObjs = Physics.OverlapSphere(transform.position, blastRadius, affectedLayers);
        foreach (Collider obj in expObjs)
        {
            if (obj.gameObject.TryGetComponent(out Rigidbody rigid))
                rigid.AddExplosionForce(blastForce, transform.position, blastRadius, upwardsModifier, ForceMode.Impulse);
        }
        OnPotionExploded?.Invoke(gameObject);
        transform.SetPositionAndRotation(startingTransform.position, startingTransform.rotation);
        OnPotionReset?.Invoke();
        hasTriggered = false;
    }

    public void ActivatePotion(GameObject obj)
    {
        if (Utils.IsInLayerMask(obj, unaffectedLayers)) return;
        if (Utils.IsInLayerMask(obj, affectedLayers))
        {
            if (!hasTriggered)
            {
                hasTriggered = true;
                StartCoroutine(nameof(ExplodePotion));
            }
        }
    }
}
