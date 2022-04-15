using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Variables/New Float Variable")]
public class GlobalFloat : ScriptableObject 
{
    public Action<float> OnValueChanged;
    [SerializeField] float currentValue;
    [SerializeField] bool resetOnDisable;
    [SerializeField] float resetValue;


    void OnDisable()
    {
        if (resetOnDisable)
            currentValue = resetValue;
    }

    public float Value
    {
        get { return currentValue; }
        set
        {
            currentValue = value;
            OnValueChanged?.Invoke(currentValue);
        }
    }
}
