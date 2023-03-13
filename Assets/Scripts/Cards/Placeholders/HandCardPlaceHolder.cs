using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{
    public class HandCardPlaceHolder : MonoBehaviour, ICardPlaceholder
    {
        private bool _isEmpty = true;

        public bool isEmpty { get => _isEmpty; private set => _isEmpty = value; }

        public ICard card { get; private set; }

        public void OnDrop(PointerEventData eventData)
        {
            ICard droppedCard = eventData.pointerDrag.GetComponent<ICard>();
            if (TryPlaceCard(droppedCard))
            {
                Transform cardTransform = eventData.pointerDrag.transform;
                cardTransform.GetComponentInParent<ICardPlaceholder>().RemoveCard(droppedCard);
                cardTransform.SetParent(transform);
                cardTransform.localPosition = Vector3.zero;
            }
        }

        public void RemoveCard(ICard card)
        {
            isEmpty = true;
        }

        public bool TryPlaceCard(ICard card)
        {
            if (isEmpty)
            {
                this.card = card;
                return true;
            }
            return false;
        }
    }
}
