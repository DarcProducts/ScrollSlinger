using System;
using UnityEngine;

public class LandPlot : MonoBehaviour
{
    [SerializeField] GlobalFloat MaxZLocation;
    [SerializeField] GlobalFloat MoveSpeed;
    [SerializeField] GlobalBool StartMoving;
    [SerializeField] float maxSpeed = 8;
    [SerializeField] GameObject[] objectsToActivate;
    LandPlotGenerator landGenerator;
    
    [SerializeField] Vector3 moveVector;

    void Awake() => landGenerator = GameObject.FindWithTag("LandGenerator").GetComponent<LandPlotGenerator>();

    void OnEnable()
    {
        foreach (var go in objectsToActivate)
            go.SetActive(true);
    }

    void FixedUpdate()
    {
        if (StartMoving.Value)
            MoveLand();
    }

    public void MoveLand()
    {
        if (transform.position.z >= MaxZLocation.Value)
            transform.Translate(Mathf.Clamp(MoveSpeed.Value, 0, maxSpeed) * Time.fixedDeltaTime * moveVector.normalized);
        else
        {
            if (landGenerator != null)
                landGenerator.ResetLandPlot();
            gameObject.SetActive(false);
        }
    }
}
