using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/New Vector2 Variable")]
public class GlobalVector2 : ScriptableObject 
{
    public Action<Vector2> OnValueChanged;
    [SerializeField] Vector2 currentValue;
    [SerializeField] bool resetOnDisable;
    [SerializeField] Vector2 resetValue;

    void OnDisable()
    {
        if (resetOnDisable)
            currentValue = resetValue;
    }

    public Vector2 Value
    {
        get { return currentValue; }
        set
        {
            this.currentValue = value;
            OnValueChanged?.Invoke(this.currentValue);
        }
    }
}
