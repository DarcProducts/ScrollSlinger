using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class LevitatePotion : Potion, IPotion
{
    [SerializeField] float levitateTime;
    [SerializeField] float levitateForce;
    [SerializeField] UnityEvent OnFinishedLevitating;
    GameObject objectToLevitate;
    Renderer rend;
    Rigidbody otherRigid;
    bool isLevitating;
    Rigidbody thisRigid;
    Collider[] potionColliders;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        thisRigid = GetComponent<Rigidbody>();
        potionColliders = GetComponentsInChildren<Collider>();
    }

    void OnEnable()
    {
        potionType = this;
    }

    void OnDisable() => StopCoroutine(LevitateObject());

    void FixedUpdate()
    {
        if (!isLevitating || objectToLevitate == null) return;
        if (otherRigid != null)
        {
            otherRigid.useGravity = false;
            otherRigid.AddForce(levitateForce * Time.fixedDeltaTime * Vector3.up);
        }
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

    IEnumerator LevitateObject()
    {
        SetCollidersEnabled(false);
        rend.enabled = false;
        isLevitating = true;
        yield return new WaitForSeconds(levitateTime);
        if (otherRigid != null)
            otherRigid.useGravity = true;
        OnFinishedLevitating?.Invoke();
        ResetPotion();
    }

    public void ResetPotion()
    {
        SwitchToBag();
        transform.SetPositionAndRotation(startingTransform.position, startingTransform.rotation);
        isLevitating = false;
        objectToLevitate = null;
        rend.enabled = true;
        SetCollidersEnabled(true);
        OnPotionReset?.Invoke();
    }

    public void ActivatePotion(GameObject obj)
    {
        if (Utils.IsInLayerMask(obj, unaffectedLayers))
        {
            OnHitUnaffectedLayer.Invoke();
            ResetPotion();
            return;
        }
        if (Utils.IsInLayerMask(obj, affectedLayers))
        {
            OnHitAffectedLayer.Invoke();
            objectToLevitate = obj;
            otherRigid = obj.GetComponent<Rigidbody>();
            StartCoroutine(LevitateObject());
        }
    }
}