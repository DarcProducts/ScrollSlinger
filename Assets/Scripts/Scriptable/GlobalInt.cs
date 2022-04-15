using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Variables/New Int Variable")]
public class GlobalInt : ScriptableObject
{
    public Action<int> OnValueChanged;
    [SerializeField] int currentValue;
    [SerializeField] bool resetOnDisable;
    [SerializeField] int resetValue;

    void OnDisable()
    {
        if (resetOnDisable)
            currentValue = resetValue;
    }

    public int Value
    {
        get { return currentValue; }
        set
        {
            currentValue = value;
            OnValueChanged?.Invoke(currentValue);
        }
    }
}
