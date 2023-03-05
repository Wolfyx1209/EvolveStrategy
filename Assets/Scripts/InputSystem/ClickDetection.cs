using EventBusSystem;
using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    private InputManager _inputManager;

    private Vector2 _startRightClickPosition;
    private float _startRightClickTime;


    private Vector2 _startLeftClickPosition;
    private float _startLeftClickTime;

    [SerializeField] private float clickZone = 30f;
    [SerializeField] private float clickMaxDuration = 2f;

    private void OnEnable()
    {
        _inputManager = InputManager.instance;
        _inputManager.OnStartLeftMouseTouch += StartLeftClick;
        _inputManager.OnEndLeftMouseTouch += EndLeftClick;

        _inputManager.OnStartRightMouseTouch += StartRightClick;
        _inputManager.OnEndRightMouseTouch += EndRightClick;
    }

    private void OnDisable()
    {
        _inputManager.OnStartLeftMouseTouch -= StartLeftClick;
        _inputManager.OnEndLeftMouseTouch -= EndLeftClick;

        _inputManager.OnStartRightMouseTouch -= StartRightClick;
        _inputManager.OnEndRightMouseTouch -= EndRightClick;
    }

    private void StartLeftClick(Vector3 startPosition, float startTime)
    {
        _startLeftClickPosition = startPosition;
        _startLeftClickTime = startTime;
    }

    private void EndLeftClick(Vector3 endPosition, float endTime)
    {
        EventBus.RaiseEvent<IClickHandler>(it => it.LeftClick(endPosition));
    }

    private void StartRightClick(Vector3 startPosition, float startTime)
    {
        _startRightClickPosition = startPosition;
        _startRightClickTime = startTime;
    }

    private void EndRightClick(Vector3 endPosition, float endTime)
    {
        if (Vector3.Distance(_startRightClickPosition, endPosition) <= clickZone &&
            _startRightClickTime - endTime <= clickMaxDuration)
        {
            EventBus.RaiseEvent<IClickHandler>(it => it.RightClick(endPosition));
        }
    }
}
