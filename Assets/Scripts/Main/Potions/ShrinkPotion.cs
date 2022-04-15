using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ShrinkPotion : Potion, IPotion
{
    [SerializeField] float shrinkTime;
    [SerializeField, Range(0f, 1f)] float scaleDecreaseRate = .1f;
    [SerializeField] float massDecreaseRate;
    [SerializeField] UnityEvent OnFinishedShrinking;
    GameObject objectToShrink;
    Vector3 objMag;
    Vector3 scale;
    bool isShrinking;
    bool hasBeenActivated;
    Rigidbody targetRigid;
    Rigidbody thisRigid;
    Collider[] potionColliders;

    void Awake()
    {
        thisRigid = GetComponent<Rigidbody>();
        potionColliders = GetComponentsInChildren<Collider>();
    }

    void OnEnable()
    {
        potionType = this;
        hasBeenActivated = false;
    }

    void OnDisable() => StopCoroutine(ShrinkObject());

    void FixedUpdate()
    {
        if (!isShrinking || objectToShrink == null) return;
        scale = objectToShrink.transform.localScale;
        objectToShrink.transform.localScale = Vector3.MoveTowards(scale, objMag, scaleDecreaseRate * Time.fixedDeltaTime);
        if (targetRigid != null)
        {
            if (targetRigid.mass > 0)
                targetRigid.mass -= Time.fixedDeltaTime * massDecreaseRate;
        }
    }

    IEnumerator ShrinkObject()
    {
        isShrinking = true;
        yield return new WaitForSeconds(shrinkTime);
        OnFinishedShrinking?.Invoke();
        ResetPotion();
    }

    public void ResetPotion()
    {
        SwitchToBag();
        transform.SetPositionAndRotation(startingTransform.position, startingTransform.rotation);
        isShrinking = false;
        targetRigid = null;
        objectToShrink = null;
        OnPotionReset?.Invoke();
    }

    void SetCollidersEnabled(bool value)
    {
        foreach (var c in potionColliders)
            c.enabled = value;
    }

    void SwitchToBag()
    {
        thisRigid.useGravity = false;
        thisRigid.isKinematic = true;
    }

    public void SetPotionToActive() => hasBeenActivated = false;

    public void ActivatePotion(GameObject obj)
    {
        if (!hasBeenActivated)
        {
            if (Utils.IsInLayerMask(obj, unaffectedLayers))
            {
                OnHitUnaffectedLayer.Invoke();
                return;
            }
            else if (Utils.IsInLayerMask(obj, affectedLayers))
            {
                OnHitAffectedLayer.Invoke();
                targetRigid = obj.GetComponent<Rigidbody>();
                objectToShrink = obj;
                objMag = objectToShrink.transform.localScale * .5f;
                hasBeenActivated = true;
                StartCoroutine(ShrinkObject());
            }
        }
    }
}