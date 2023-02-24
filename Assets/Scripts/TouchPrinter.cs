using EventBusSystem;
using UnityEngine;

public class TouchPrinter : MonoBehaviour, ISwipeHandler
{
    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
    public void Swipe(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        Debug.Log("Start: " + swipeStartPosition + " End: " + swipeEndPosition);
    }
}
