using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem 
{ 
    [CreateAssetMenu(fileName = "CardData", menuName = "Cards/New CardData")]
    public class CardData : ScriptableObject
    {
        [Header("General:")]
        public CardType cardType;
        public string title;
        public int number;
        public Sprite image;

        [Header("Bonuses:")]
        public int attackBonus;
        public int defenseBonus;
        public float moveSpeedBonus;
        public float spawnSpeedBonus;
        public float swimSpeedTimeBonus;
        public float climbSpeedBonus;
        public float coldResistanceBonus;
        public float heatResistanceBonus;
        public float poisonResistanceBonus;
    }
}

