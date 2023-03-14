using EventBusSystem;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem 
{ 
    public class CardInHand : MonoBehaviour, ICardBoughtHandler
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

        private void OnDestroy()
        {
            EventBus.Unsubscribe(this);
        }
        private void AddCard(CardData data)
        {
            HandCardPlaceHolder newPlaceholder = Instantiate(placeholderAsset, cardZome).GetComponent<HandCardPlaceHolder>();
            Card newCard = Instantiate(cardAsset, newPlaceholder.transform).GetComponent<Card>();
            newCard.InstateCard(data);
            newPlaceholder.TryPlaceCard(newCard);
            cardsInHand.Add(newCard, newPlaceholder);
        }

        private void DeleteCard(ICard card)
        {
            cardsInHand.Remove(card);
        }

        public void CardBought(CardData card)
        {
            AddCard(card);
        }
    }
}


