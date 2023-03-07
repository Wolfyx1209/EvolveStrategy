using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TileSystem;
using BattleSystem;
using EventBusSystem;

public class BattleBot : MonoBehaviour, ICellChangeOwnerHandler
{
    private TerrainTilemap _tilemap;
    private BattleManager _battleManager;

    private List<TerrainCell> _myCells;

    [SerializeField] private PlayersList _me;
    [SerializeField] private float TimeToOneTurn;

    private void Start()
    {
        _tilemap = FindObjectOfType<TerrainTilemap>();
        _battleManager = BattleManager.instance;
        _myCells = _tilemap.GetAllCellsOfOnePlayer(_me);
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
            if(currentCell.owner == _me) 
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
                _battleManager.TryGiveOrderToAttackAllUnit(cell, enemyMinCell);
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
                _battleManager.TryGiveOrderToAttackHalfUnit(cell, friendMaxCell);
            }
        }
    }

    public void ChangeOwner(PlayersList previousOwner, PlayersList newOwner, TerrainCell cell)
    {
        if(previousOwner == _me && _myCells.Contains(cell)) 
        { 
            _myCells.Remove(cell);
        }
        if(newOwner == _me && !_myCells.Contains(cell)) 
        { 
            _myCells.Add(cell);
        }
    }
}
