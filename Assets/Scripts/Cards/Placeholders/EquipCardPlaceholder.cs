using UnityEngine;
using EventBusSystem;
using UnityEngine.EventSystems;

namespace CardSystem
{
    public class EquipCardPlaceholder : MonoBehaviour, ICardPlaceholder, IDropHandler
    {
        private bool _isEmpty = true;
        public CardType slotType { get; private set; }

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
                EventBus.RaiseEvent<ICardEquipedHandler>(it => it.CardEquiped(droppedCard, card));
                card = droppedCard;
            }
        }

        public void RemoveCard(ICard card)
        {
            isEmpty = true;
        }

        public bool TryPlaceCard(ICard card)
        {
            if (isEmpty && slotType == card.cardData.cardType)
            {
                return true;
            }
            return false;
        }
    }
}

