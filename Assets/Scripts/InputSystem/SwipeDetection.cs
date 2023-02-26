using UnityEngine;
using EventBusSystem;
public class SwipeDetection : MonoBehaviour
{
    private InputManager _inputManager => GetComponent<InputManager>();

    private Vector2 _startSwipePosition;
    private float _startSwipeTime;

    public float swipeDeadZone = 1f;
    public float swipeMaxDuration = 2f;

    private void OnEnable()
    {
        _inputManager.OnStartTouch += StartSwipe;
        _inputManager.OnEndTouch += EndSwipe;
    }

    private void OnDisable()
    {
        _inputManager.OnStartTouch -= StartSwipe;
        _inputManager.OnEndTouch -= EndSwipe;
    }

    private void StartSwipe(Vector3 startPosition, float startTime)
    {
        _startSwipePosition = startPosition;
        _startSwipeTime = startTime;
        LineRenderer lr = new();
    }

    private void EndSwipe(Vector3 endPosition, float endTime)
    {
        if(Vector3.Distance(_startSwipePosition, endPosition) >= swipeDeadZone && 
            _startSwipeTime - endTime <= swipeMaxDuration) 
        {
            EventBus.RaiseEvent<ISwipeHandler>(it => it.Swipe(_startSwipePosition, endPosition));
        }
    }
}
