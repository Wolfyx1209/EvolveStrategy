using EventBusSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Bank : Singletone<Bank>
{
    private Dictionary<PlayersList, int> _playersPoints = new();

    private void Awake()
    {
        Debug.Log(1);
        GameAcktor[] acktors = FindObjectsOfType<GameAcktor>();
        foreach (GameAcktor acktor in acktors) 
        {
            Debug.Log(acktor.acktorName);
            _playersPoints.Add(acktor.acktorName, 0);
        }
    }
    public bool TryToBuy(PlayersList acktor, int cost)
    {
        if (cost < 0)
        {
            throw new Exception("The price cannot be negative");
        }
        if (_playersPoints[acktor] - cost > 0)
        {
            _playersPoints[acktor] -= cost;
            EventBus.RaiseEvent<IEvolvePointsChangeHandler>(it => it.EvolvePointsChanges(acktor, cost));

            return true;
        }
        return false;
    }

    public void AddPoints(PlayersList acktor, int value)
    {
        _playersPoints[acktor] += value;
        EventBus.RaiseEvent<IEvolvePointsChangeHandler>(it => it.EvolvePointsChanges(acktor, value));
    }

    public int GetAcktorPoints(PlayersList acktor) 
    {
        return _playersPoints[acktor];
    }
}
