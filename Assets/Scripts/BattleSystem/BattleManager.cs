using EventBusSystem;
using System.Collections.Generic;
using TileSystem;
using UnityEngine;

namespace BattleSystem
{
    public class BattleManager : MonoBehaviour, ISwipeHandler
    {
        private OrderDrawer OrderDrawer => 
            FindObjectOfType<OrderDrawer>();
        private TerrainTilemap _terrainTilemap =>
            FindObjectOfType<TerrainTilemap>();

        private List<IComand> _comandList = new();

        private const float timeToAttack = 2f;

        private void OnEnable()
        {
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
            OrderDrawer.NewComand(from.transform.position, to.transform.position, attackCell);
        }

        public void Swipe(Vector3 swipeStartPosition, Vector3 swipeEndPosition)
        {
            if (_terrainTilemap.ContainTile(swipeStartPosition)
                && _terrainTilemap.ContainTile(swipeEndPosition))
            { 
                TerrainCell from = _terrainTilemap.GetTile(swipeStartPosition);
                TerrainCell to = _terrainTilemap.GetTile(swipeEndPosition);
                if(_terrainTilemap.isCellsNeighbours(to, from) && from.unitNumber / 2 >0) 
                { 
                    GiveOrderToAttack(from, to, from.unitNumber/2);
                }
            }
        }

        public void ChoseBattleSituation(TerrainCell to, Unit unit, int unitCount)
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
    }
}

