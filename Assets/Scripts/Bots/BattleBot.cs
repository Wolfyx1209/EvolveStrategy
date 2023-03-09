using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TileSystem;
using BattleSystem;
using EventBusSystem;

public class BattleBot : GameAcktor, ICellChangeOwnerHandler
{
    private TerrainTilemap _tilemap;
    private BattleManager _battleManager;

    private List<TerrainCell> _myCells;

    [SerializeField] private PlayersList _me;
    [SerializeField] private float TimeToOneTurn;

    private new void Awake()
    {
        base.Awake();
        _tilemap = FindObjectOfType<TerrainTilemap>();
        _battleManager = BattleManager.instance;
        _myCells = _tilemap.GetAllCellsOfOnePlayer(this);
        EventBus.Subscribe(this);

        StartCoroutine(AnalysisSituationAndMakeTurn());
    }

    IEnumerator AnalysisSituationAndMakeTurn() 
    {
        while(_myCells.Count > 0) 
        {
            foreach(TerrainCell cell in _myCells) 
            {
                MakeDesigion(cell);
            }
            yield return new WaitForSeconds(TimeToOneTurn);
        }
    }

    private void MakeDesigion(TerrainCell cell) 
    { 
        List<TerrainCell> cellToAnalysis = _tilemap.GetCellNeighbors(cell);
        TerrainCell friendMaxCell = null;
        TerrainCell enemyMinCell = null;
        TerrainCell enemyMaxCell = null;
        foreach (TerrainCell currentCell in cellToAnalysis) 
        { 
            if(currentCell.owner == this) 
            { 
                if(friendMaxCell == null)
                { 
                    friendMaxCell = currentCell;
                }
                else 
                { 
                    if(friendMaxCell.unitNumber < currentCell.unitNumber) 
                    {
                        friendMaxCell = currentCell;
                    }
                }
            }
            else 
            {
                if (enemyMinCell == null)
                {
                    enemyMinCell = currentCell;
                }
                else 
                { 
                    if(enemyMinCell.unitNumber > currentCell.unitNumber) 
                    {
                        enemyMinCell = currentCell;
                    }
                }
            }
        }
        if(enemyMinCell != null) 
        {
            if(enemyMinCell.unitNumber < cell.unitNumber) 
            {
                _battleManager.TryGiveOrderToAttackAllUnit(cell, enemyMinCell, this);
                return;
            }
        }
        if(enemyMaxCell != null) 
        { 
            if(enemyMaxCell.unitNumber > cell.unitNumber) 
            { 
                return;
            }
        }
        if(friendMaxCell != null) 
        { 
            if(cell.unitNumber > 10 && cell.unitNumber < friendMaxCell.unitNumber) 
            {
                _battleManager.TryGiveOrderToAttackHalfUnit(cell, friendMaxCell, this);
            }
        }
    }

    public void ChangeOwner(GameAcktor previousOwner, GameAcktor newOwner, TerrainCell cell)
    {
        if(previousOwner == this && _myCells.Contains(cell)) 
        { 
            _myCells.Remove(cell);
        }
        if(newOwner == this && !_myCells.Contains(cell)) 
        { 
            _myCells.Add(cell);
        }
    }


    public override void OfferToBuildNest(Region region)
    {
        throw new System.NotImplementedException();
    }
}
