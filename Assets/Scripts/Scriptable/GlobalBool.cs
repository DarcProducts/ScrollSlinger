using UnityEngine;
using System;
[CreateAssetMenu(menuName = "Variables/New Bool Variable")]
public class GlobalBool : ScriptableObject 
{ 
    [SerializeField] bool currentValue; 
    public Action<bool> OnValueChanged;
    [SerializeField] bool resetOnDisable;
    [SerializeField] bool resetValue;
    public bool Value { 
        get { return currentValue; } 
        set {
            currentValue = value;
            OnValueChanged?.Invoke(currentValue);
        } 
    }

    void OnDisable()
    {
        if (resetOnDisable)
            currentValue = resetValue;
    }
}
