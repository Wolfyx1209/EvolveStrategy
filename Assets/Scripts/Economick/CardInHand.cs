using EventBusSystem;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem 
{ 
    public class CardInHand : MonoBehaviour, ICardEquipedHandler
    {
        [SerializeField] private Transform cardZome;
        private GameObject cardAsset;
        private GameObject placeholderAsset;
        private Dictionary<ICard, HandCardPlaceHolder> cardsInHand = new();
        private void Awake()
        {
            cardAsset = (GameObject)Resources.Load("Cards/CardPrefab/MovableCard");
            placeholderAsset = (GameObject)Resources.Load("Cards/CardPrefab/CardPlaseHolder");
            EventBus.Subscribe(this);
        }
        public void AddCard(CardData data)
        {
            HandCardPlaceHolder newPlaceholder = Instantiate(placeholderAsset, cardZome).GetComponent<HandCardPlaceHolder>();
            Card newCard = Instantiate(cardAsset, newPlaceholder.transform).GetComponent<Card>();
            newCard.InstateCard(data);
            cardsInHand.Add(newCard, newPlaceholder);
        }

        private void DeleteCard(ICard card)
        {
            cardsInHand.Remove(card);
        }

        public void CardEquiped(ICard card, ICard previousCard)
        {
            if (cardsInHand.ContainsKey(card)) 
            {
                DeleteCard(card);
                Destroy(cardsInHand[card].gameObject);
            }
        }
    }
}


