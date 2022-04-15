using UnityEngine;
using UnityEngine.InputSystem;

public class XRController : MonoBehaviour
{
    [SerializeField] InputActionReference leftTrigger;
    [SerializeField] float trigger;

    void OnEnable()
    {
        leftTrigger.action.performed += UpdateTriggerValue;
    }

    void OnDisable()
    {
        leftTrigger.action.performed -= UpdateTriggerValue;
    }

    void UpdateTriggerValue(InputAction.CallbackContext ctx)
    {
        float tVal = ctx.ReadValue<float>();
        tVal = tVal > 1 ? 1 : tVal < 0 ? 0 : tVal;
        trigger = tVal;
    }
}