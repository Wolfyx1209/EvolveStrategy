using UnityEngine;

namespace EventBusSystem 
{ 
    public interface ISwipeHandler : IGlobalSubscriber
    {
        public void Swipe(Vector3 swipeStartPosition, Vector3 swipeEndPosition);
    }
}
