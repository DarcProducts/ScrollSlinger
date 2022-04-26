using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class OculusQuestControllerSetup : MonoBehaviour
{
    [Header("Left Hand"), HorizontalLine]
    public GlobalVector2 leftThumbstick;

    public GlobalBool leftHandTriggerActive;
    public GlobalBool leftHandGripActive;
    public GlobalBool xButtonActivated;
    public GlobalBool yButtonActivated;
    public GlobalBool menuButtonActivated;
    [Header("Left Hand Action References")]
    [SerializeField] InputActionReference leftHandThumbstick;

    [SerializeField] InputActionReference leftHandTriggerAction;
    [SerializeField] InputActionReference leftHandGripAction;
    [SerializeField] InputActionReference xButtonAction;
    [SerializeField] InputActionReference yButtonAction;
    public InputActionReference menuButton;

    [Header("Right Hand"), HorizontalLine]
    public GlobalVector2 rightThumbstick;

    public GlobalBool rightHandTriggerActive;
    public GlobalBool rightHandGripActive;
    public GlobalBool aButtonActivated;
    public GlobalBool bButtonActivated;
    [Header("Right Hand Action References")]
    [SerializeField] InputActionReference rightHandThumbstick;

    [SerializeField] InputActionReference rightHandTriggerAction;
    [SerializeField] InputActionReference rightHandGripAction;
    [SerializeField] InputActionReference aButtonAction;
    [SerializeField] InputActionReference bButtonAction;

    void OnEnable()
    {
        leftHandThumbstick.action.performed += LeftHandThumbPress;
        leftHandTriggerAction.action.performed += LeftTriggerPress;
        leftHandGripAction.action.performed += LeftGripPress;
        rightHandThumbstick.action.performed += RightHandThumbstick;
        rightHandTriggerAction.action.performed += RightTriggerPress;
        rightHandGripAction.action.performed += RightGripPress;
        aButtonAction.action.performed += AButtonPress;
        bButtonAction.action.performed += BButtonPress;
        xButtonAction.action.performed += XButtonPress;
        yButtonAction.action.performed += YButtonPress;
        menuButton.action.performed += MenuButtonPressed;
    }

    void OnDisable()
    {
        leftHandThumbstick.action.performed -= LeftHandThumbPress;
        leftHandTriggerAction.action.performed -= LeftTriggerPress;
        leftHandGripAction.action.performed -= LeftGripPress;
        rightHandThumbstick.action.performed -= RightHandThumbstick;
        rightHandTriggerAction.action.performed -= RightTriggerPress;
        rightHandGripAction.action.performed -= RightGripPress;
        aButtonAction.action.performed -= AButtonPress;
        bButtonAction.action.performed -= BButtonPress;
        xButtonAction.action.performed -= XButtonPress;
        yButtonAction.action.performed -= YButtonPress;
        menuButton.action.performed -= MenuButtonPressed;
    }

    void MenuButtonPressed(InputAction.CallbackContext obj) => menuButtonActivated.Value = obj.ReadValueAsButton();


    void XButtonPress(InputAction.CallbackContext obj) => xButtonActivated.Value = obj.ReadValueAsButton();

    void YButtonPress(InputAction.CallbackContext obj) => yButtonActivated.Value = obj.ReadValueAsButton();


    void AButtonPress(InputAction.CallbackContext obj) => aButtonActivated.Value = obj.ReadValueAsButton();


    void BButtonPress(InputAction.CallbackContext obj) => bButtonActivated.Value = obj.ReadValueAsButton();


    void LeftHandThumbPress(InputAction.CallbackContext obj) => leftThumbstick.Value = obj.ReadValue<Vector2>();

    void RightHandThumbstick(InputAction.CallbackContext obj) => rightThumbstick.Value = obj.ReadValue<Vector2>();

    void LeftTriggerPress(InputAction.CallbackContext obj) => leftHandTriggerActive.Value = obj.ReadValueAsButton();

    void LeftGripPress(InputAction.CallbackContext obj) => leftHandGripActive.Value = obj.ReadValueAsButton();

    void RightTriggerPress(InputAction.CallbackContext obj) => rightHandTriggerActive.Value = obj.ReadValueAsButton();

    void RightGripPress(InputAction.CallbackContext obj) => rightHandGripActive.Value = obj.ReadValueAsButton();

}