using EventBusSystem;
using System.Collections.Generic;
using TileSystem;
using UnityEngine;

namespace BattleSystem
{
    public class BattleManager : Singletone<BattleManager>, ISwipeHandler, IClickHandler
    {
        private OrderDrawer _orderDrawer;

        private TerrainTilemap _terrainTilemap;

        private NestBuilder _nestBuilder;

        private List<IComand> _comandList = new();

        private const float timeToAttack = 2f;

        private void OnEnable()
        {
            if (_orderDrawer == null) _orderDrawer = FindObjectOfType<OrderDrawer>();
            if(_terrainTilemap == null) _terrainTilemap = FindObjectOfType<TerrainTilemap>();
            if (_nestBuilder == null) _nestBuilder = new NestBuilder();
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
        }
        public void GiveOrderToAttack(TerrainCell from, TerrainCell to, int unitsSent)
        {
            Unit attackingUnit = PlayersUnits.GetUnit(from.owner);
            AttackCell attackCell = new(to, attackingUnit, unitsSent ,timeToAttack * attackingUnit.speed);
            from.unitNumber -= unitsSent;
            attackCell.OnAttackEnd += ChoseBattleSituation;
            attackCell.OnComandEnd += RemoveComand;
            _comandList.Add(attackCell);
            _orderDrawer.NewComand(from.transform.position, to.transform.position, attackCell);
        }

        public void RightSwipe(Vector3 swipeStartPosition, Vector3 swipeEndPosition)
        {
            if (IsÑonditionsÑorrect(swipeStartPosition, swipeEndPosition))
            {
                TerrainCell from = _terrainTilemap.GetTile(swipeStartPosition);
                TerrainCell to = _terrainTilemap.GetTile(swipeEndPosition);
                GiveOrderToAttack(from, to, from.unitNumber / 2);
            }
        }
        public void LeftSwipe(Vector3 swipeStartPosition, Vector3 swipeEndPosition)
        {
            if(IsÑonditionsÑorrect(swipeStartPosition, swipeEndPosition)) 
            {
                TerrainCell from = _terrainTilemap.GetTile(swipeStartPosition);
                TerrainCell to = _terrainTilemap.GetTile(swipeEndPosition);
                GiveOrderToAttack(from, to, from.unitNumber);
            }
        }

        private bool IsÑonditionsÑorrect(Vector3 cellA, Vector3 cellB) 
        {
            if (_terrainTilemap.ContainTile(cellA)
                && _terrainTilemap.ContainTile(cellB))
            {
                TerrainCell from = _terrainTilemap.GetTile(cellA);
                TerrainCell to = _terrainTilemap.GetTile(cellB);
                if (_terrainTilemap.isCellsNeighbours(to, from) && from.unitNumber >= 1)
                {
                    return true;
                }
            }
            return false;
        }

        private void ChoseBattleSituation(TerrainCell to, Unit unit, int unitCount)
        {
            if (unit.owner != to.owner)
            {
                IntatiateBattle(to, unitCount, unit);
            }
            else
            {
                to.unitNumber += unitCount;
            }

        }

        private void IntatiateBattle(TerrainCell cell, int attackUnitCount, Unit unit)
        {
            if (cell.unitNumber - attackUnitCount < 0)
            {
                cell.owner = unit.owner;
            }
            cell.unitNumber = Mathf.Abs(cell.unitNumber - attackUnitCount);
        }

        private void RemoveComand(IComand comand)
        {
            _comandList.Remove(comand);
        }

        public void RightClick(Vector3 position)
        {

        }

        public void LeftClick(Vector3 position)
        {
            Debug.Log("gg");
            if (_terrainTilemap.ContainTile(position)) 
            {
                if (_nestBuilder.TryBuildNest(_terrainTilemap.GetTile(position))) 
                {
                    EventBus.RaiseEvent<IPlayerChoosesNestCellHandler>(it => it.EndState(_terrainTilemap.GetTile(position).region));
                }
            }
        }
    }
}

