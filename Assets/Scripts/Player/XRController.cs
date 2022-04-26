using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class XRController : MonoBehaviour
{
    [Header("LEFT HAND"), HorizontalLine]
    [SerializeField] InputActionReference leftTrigger;
    [SerializeField] bool triggerPressedL;
    [SerializeField] InputActionReference leftGrip;
    [SerializeField] bool gripSqueezedL;
    [Header("RIGHT HAND"), HorizontalLine]
    [SerializeField] InputActionReference rightTrigger;
    [SerializeField] bool triggerPressedR;
    [SerializeField] InputActionReference rightGrip;
    [SerializeField] bool gripSqueezedR;

    void OnEnable()
    {
        leftTrigger.action.performed += UpdateLTriggerValue;
        leftGrip.action.performed += UpdateLGripValue;
        rightTrigger.action.performed += UpdateRTriggerValue;
        rightGrip.action.performed += UpdateRGripValue;
    }

    void OnDisable()
    {
        leftTrigger.action.performed -= UpdateLTriggerValue;
        leftGrip.action.performed -= UpdateLGripValue;
        rightTrigger.action.performed -= UpdateRTriggerValue;
        rightGrip.action.performed -= UpdateRGripValue;
    }

    void UpdateLTriggerValue(InputAction.CallbackContext ctx) => triggerPressedL = ctx.ReadValueAsButton();
    void UpdateRTriggerValue(InputAction.CallbackContext ctx) => triggerPressedR = ctx.ReadValueAsButton();

    void UpdateLGripValue(InputAction.CallbackContext ctx) => gripSqueezedL = ctx.ReadValueAsButton();

    void UpdateRGripValue(InputAction.CallbackContext ctx) => gripSqueezedR = ctx.ReadValueAsButton();
}