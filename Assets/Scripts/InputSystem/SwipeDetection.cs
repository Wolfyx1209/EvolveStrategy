using UnityEngine;
using EventBusSystem;
public class SwipeDetection : MonoBehaviour
{
    private InputManager _inputManager;

    private Vector2 _startRightSwipePosition;
    private float _startRightSwipeTime;


    private Vector2 _startLeftSwipePosition;
    private float _startLeftSwipeTime;

    [SerializeField] private float swipeDeadZone = 1f;
    [SerializeField] private float swipeMaxDuration = 2f;

    private void OnEnable()
    {
        _inputManager = InputManager.instance;
        _inputManager.OnStartLeftMouseTouch += StartLeftSwipe;
        _inputManager.OnEndLeftMouseTouch += EndLeftSwipe;

        _inputManager.OnStartRightMouseTouch += StartRightSwipe;
        _inputManager.OnEndRightMouseTouch += EndRightSwipe;
    }

    private void OnDisable()
    {
        _inputManager.OnStartLeftMouseTouch -= StartLeftSwipe;
        _inputManager.OnEndLeftMouseTouch -= EndLeftSwipe;

        _inputManager.OnStartRightMouseTouch -= StartRightSwipe;
        _inputManager.OnEndRightMouseTouch -= EndRightSwipe;
    }

    private void StartLeftSwipe(Vector3 startPosition, float startTime)
    {
        _startLeftSwipePosition = startPosition;
        _startLeftSwipeTime = startTime;
    }

    private void EndLeftSwipe(Vector3 endPosition, float endTime)
    {
        if(Vector3.Distance(_startLeftSwipePosition, endPosition) >= swipeDeadZone && 
            _startLeftSwipeTime - endTime <= swipeMaxDuration) 
        {
            EventBus.RaiseEvent<ISwipeHandler>(it => it.LeftSwipe(_startLeftSwipePosition, endPosition));
        }
    }

    private void StartRightSwipe(Vector3 startPosition, float startTime)
    {
        _startRightSwipePosition = startPosition;
        _startRightSwipeTime = startTime;
    }

    private void EndRightSwipe(Vector3 endPosition, float endTime)
    {
        if (Vector3.Distance(_startRightSwipePosition, endPosition) >= swipeDeadZone &&
            _startRightSwipeTime - endTime <= swipeMaxDuration)
        {
            EventBus.RaiseEvent<ISwipeHandler>(it => it.RightSwipe(_startRightSwipePosition, endPosition));
        }
    }
}
