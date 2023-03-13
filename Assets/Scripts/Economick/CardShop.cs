using CardSystem;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardShopView))]
public class CardShop : MonoBehaviour
{
    [SerializeField] private CardInHand hand;
    public CardData[] StartCards;

    private List<CardData> _avalibleForSell = new();

    private List<CardData> _currentCards;

    private Bank bank;
    private CardShopView view;

    [SerializeField] private int cardInRoll; 

    private void Start()
    {
        view = GetComponent<CardShopView>();
        bank = Bank.instance;
        foreach (CardData card in StartCards)
        {
            _avalibleForSell.Add(card);
        }
        RerollCurrentCards();
    }

    public List<CardData> RerollCurrentCards()
    {
        List<CardData> notSelectedCards = _avalibleForSell;
        List<CardData> selectedCards = new();
        int sampleLength = cardInRoll > notSelectedCards.Count ? notSelectedCards.Count : cardInRoll;
        for (int i =0; i < sampleLength; i++) 
        { 
            int index = Random.Range(0, notSelectedCards.Count -1);
            selectedCards.Add(notSelectedCards[index]);
            notSelectedCards.Remove(notSelectedCards[index]);
        }
        _currentCards = selectedCards;
        view.RefillCards(selectedCards);
        return selectedCards;
    }

    public bool TryByCard(CardData card) 
    {
        if (bank.TryToBuy(card.cost)) 
        {
            AddNewCardsToSellPull(card);
            AddCardToHand(card);
            return true;
        }
        return false;
    }
    private void AddNewCardsToSellPull(CardData card) 
    { 
        foreach(CardData unlockedCard in card.openCards) 
        {
            if (!_avalibleForSell.Contains(unlockedCard)) 
            {
                _avalibleForSell.Add(unlockedCard);
            }
        }
    }

    private void AddCardToHand(CardData card)   
    {
        hand.AddCard(card);
    }
}
