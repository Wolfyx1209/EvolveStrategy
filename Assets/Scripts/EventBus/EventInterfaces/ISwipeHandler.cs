using UnityEngine;

namespace EventBusSystem 
{ 
    public interface ISwipeHandler : IGlobalSubscriber
    {
        public void Swipe(Vector2 swipeStartPosition, Vector2 swipeEndPosition);
    }
}
