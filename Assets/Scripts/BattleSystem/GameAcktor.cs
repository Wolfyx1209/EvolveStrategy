using System.Collections;
using System.Collections.Generic;
using TileSystem;
using UnityEngine;
using BattleSystem;

public abstract class GameAcktor : MonoBehaviour
{
    [SerializeField] public PlayersList acktorName { get; protected set; }
    [SerializeField] public Unit unit { get; protected set; }

    protected void Awake()
    {
        unit = new(this);
    }
    public abstract void OfferToBuildNest(Region region);
}
