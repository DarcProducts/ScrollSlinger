using System;
using UnityEngine;

public class LandPlot : MonoBehaviour
{
    [SerializeField] GlobalFloat MaxZLocation;
    [SerializeField] GlobalFloat MoveSpeed;
    [SerializeField] GlobalBool StartMoving;
    [SerializeField] float maxSpeed = 8;
    LandPlotGenerator _landGenerator;
    
    [SerializeField] Vector3 moveVector;

    void Awake() => _landGenerator = GameObject.FindWithTag("LandGenerator").GetComponent<LandPlotGenerator>();

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
            if (_landGenerator != null)
                _landGenerator.ResetLandPlot();
            gameObject.SetActive(false);
        }
    }
}
