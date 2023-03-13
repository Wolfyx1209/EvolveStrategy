using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{
    public class MovableCard : Card, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private CanvasGroup _canvasGroup;
        private Canvas _canvas;

        private new void Start()
        {
            base.Start();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            Transform slotTransform = _rectTransform.parent.transform;
            slotTransform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            _rectTransform.localPosition = Vector3.zero;
        }

        public override void InstateCard(CardData data)
        {
            base.InstateCard(data);
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponentInParent<CanvasGroup>();
        }
    }
}

