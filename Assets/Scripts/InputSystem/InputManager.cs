using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);
    public event StartTouch OnEndTouch;
    #endregion

    private Camera mainCamera => Camera.main;
    private TouchControl inputActions;

    private void Awake()
    {
        inputActions = new TouchControl();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        inputActions.Touch.PrimaryContact.started += ctx => StartPrimaryContact(ctx);
        inputActions.Touch.PrimaryContact.canceled += ctx => EndPrimaryContact(ctx);
    }

    private void StartPrimaryContact(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            Vector3 contactPosition = inputActions.Touch.PrimaryPosition.ReadValue<Vector2>();
            OnStartTouch(mainCamera.ScreenToWorldPoint(contactPosition), (float)context.startTime);
        }
    }

    private void EndPrimaryContact(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            Vector3 contactPosition = inputActions.Touch.PrimaryPosition.ReadValue<Vector2>();
            OnEndTouch(mainCamera.ScreenToWorldPoint(contactPosition), (float)context.time);
        }
    }
}
