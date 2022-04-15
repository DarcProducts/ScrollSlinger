using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GrowPotion : Potion, IPotion
{
    [SerializeField] float growTime;
    [SerializeField, Range(0f, 1f)] float scaleIncreaseRate = .1f;
    [SerializeField] float massIncreaseRate;
    [SerializeField] UnityEvent OnFinshedGrowing;
    GameObject objectToGrow;
    Renderer rend;
    bool isGrowing;
    bool hasBeenActivated;
    Rigidbody targetRigid;
    Vector3 objMag;
    Vector3 scale;

    void Awake() => rend = GetComponent<Renderer>();

    void OnEnable()
    {
        potionType = this;
        hasBeenActivated = false;
    }

    void OnDisable() => StopCoroutine(GrowObject());

    void FixedUpdate()
    {
        if (!isGrowing || objectToGrow == null) return;
        scale = objectToGrow.transform.localScale;
        objectToGrow.transform.localScale = Vector3.MoveTowards(scale, objMag, scaleIncreaseRate * Time.fixedDeltaTime);
        if (targetRigid != null)
            targetRigid.mass += Time.fixedDeltaTime * massIncreaseRate;
    }

    IEnumerator GrowObject()
    {
        rend.enabled = false;
        isGrowing = true;
        yield return new WaitForSeconds(growTime);
        OnFinshedGrowing?.Invoke();
        ResetPotion();
    }

    public void SetPotionToActive() => hasBeenActivated = false;

    public void ResetPotion()
    {
        transform.SetPositionAndRotation(startingTransform.position, startingTransform.rotation);
        isGrowing = false;
        targetRigid = null;
        objectToGrow = null;
        rend.enabled = true;
        OnPotionReset?.Invoke();
    }

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
                objectToGrow = obj;
                objMag = objectToGrow.transform.localScale * 1.5f;
                hasBeenActivated = true;
                StartCoroutine(GrowObject());
            }
        }
    }
}