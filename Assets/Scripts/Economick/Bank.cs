using EventBusSystem;
using System;
using UnityEngine;
public class Bank : Singletone<Bank>
{
    private int _evolvePoints = 1000;
    public int evolvePoints
    {
        get => _evolvePoints;
        private set
        {
            _evolvePoints = value;
            EventBus.RaiseEvent<IEvolvePointsChangeHandler>(it => it.EvolvePointsChanges(value));
        }
    }

    public bool TryToBuy(int cost)
    {
        if (cost < 0)
        {
            throw new Exception("The price cannot be negative");
        }
        if (evolvePoints - cost > 0)
        {
            evolvePoints -= cost;

            return true;
        }
        return false;
    }

    public void AddPoints(int value) 
    { 
        evolvePoints += value;
    }
}
