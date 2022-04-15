using System.Collections;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Rigidbody))]
public class Firework : MonoBehaviour
{
    [SerializeField] float launchForce;
    [SerializeField] float launchDuration;
    [SerializeField] Transform forwardDirection;
    [SerializeField] Transform startingLocation;
    [SerializeField] GameObject flightEffects;
    [SerializeField] LayerMask resetLayers;
    [SerializeField] LayerMask attachToLayers;
    [SerializeField] GameEvent FireworkExploded;
    [SerializeField] UnityEvent OnFireworkReset;
    Rigidbody rigid;
    readonly float currentVelocity;
    bool isActive;

    void Awake() => rigid = GetComponent<Rigidbody>();

    void FixedUpdate()
    {
        if (!isActive) return;
        rigid.useGravity = false;
        rigid.AddForce(launchForce * Time.fixedDeltaTime * forwardDirection.forward, ForceMode.Acceleration);
    }

    [ContextMenu("Launch Firework")]
    public void LaunchFirework()
    {
        if (flightEffects != null)
            flightEffects.SetActive(true);
        isActive = true;
        StartCoroutine(ExplodeFirework());
    }

    IEnumerator ExplodeFirework()
    {
        yield return new WaitForSeconds(launchDuration);
        isActive = false;
        FireworkExploded.Invoke(gameObject);
        OnFireworkReset?.Invoke();
    }

    public void ResetFirework()
    {
        transform.SetPositionAndRotation(startingLocation.position, startingLocation.rotation);
        flightEffects.SetActive(false);
        isActive = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (isActive) return;
        GameObject obj = collision.gameObject;
        if (Utils.IsInLayerMask(obj, resetLayers)) return;
            ResetFirework();
    }
}
