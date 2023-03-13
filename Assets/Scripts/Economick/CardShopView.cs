using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public class CardShopView : MonoBehaviour
    {
        [SerializeField] private Transform cardZome;
        private GameObject cardAsset;
        private GameObject shopElementAsset;
        private List<Card> activeCards = new();
        private void Awake()
        {
            cardAsset = (GameObject)Resources.Load("Cards/CardPrefab/CardAsset");
        }

        public void RefillCards(List<CardData> cardsData)
        {
            for(int i =0; i < cardsData.Count; i++) 
            { 
                if(activeCards.Count< i) 
                {
                    RefillExistingCard(i, cardsData[i]);
                }
                else 
                {
                    CreateNewCard(cardsData[i]);
                }
            }
            if(activeCards.Count > cardsData.Count) 
            {
                DeleteAllActiveCardAfterIndex(activeCards.Count);
            }
        }

        private void RefillExistingCard(int index, CardData data) 
        {
            activeCards[index].InstateCard(data);
        }
        private void CreateNewCard(CardData data)
        {
            Card newCard = Instantiate(cardAsset, cardZome).GetComponent<Card>();
            newCard.InstateCard(data);
            activeCards.Add(newCard);
        }

        private void DeleteAllActiveCardAfterIndex(int index) 
        { 
            for(int i = index; i < activeCards.Count; i++) 
            {
                Destroy(activeCards[i].gameObject);
            }
        }
    }

}

