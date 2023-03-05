using UnityEngine;

namespace TileSystem 
{ 
    public class NestBuildView : MonoBehaviour
    {
        public delegate void Click();
        public event Click OnClick;

        private void OnMouseUp()
        {
            OnClick.Invoke();
        }
    }
}

