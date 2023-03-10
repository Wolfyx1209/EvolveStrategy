using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem 
{
    public class CardPlaceholder : MonoBehaviour, ICardPlaceholder, IDropHandler
    {
        public CardType slotType { get; private set; }

        public bool isEmpty { get; private set; }

        public ICard card { get; private set; }

        public void OnDrop(PointerEventData eventData)
        {
            Transform cardTransform = eventData.pointerDrag.transform;
            cardTransform.SetParent(transform);
            cardTransform.localPosition = Vector3.zero;
        }

        public bool TryPlace—ard(ICard card)
        {
            if (isEmpty && slotType == card.cardType)
            {
                this.card = card;
                return true;
            }
            return false;
        }
    }
}

