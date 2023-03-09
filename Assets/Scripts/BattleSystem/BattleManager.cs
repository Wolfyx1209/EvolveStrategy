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

        private PlayersUnits _playersUnits;

        private const float DEFAULT_TIME_TO_ATTACK   = 4f;

        private void Awake()
        {
            _playersUnits = PlayersUnits.instance;
        }

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

        public void TryGiveOrderToAttackHalfUnit(TerrainCell from, TerrainCell to) 
        {
            if(IsConditionsCorrect(from, to))
                GiveOrderToAttack(from, to, from.unitNumber / 2);
        }

        public void TryGiveOrderToAttackAllUnit(TerrainCell from, TerrainCell to)
        {
            if (IsConditionsCorrect(from, to))
                GiveOrderToAttack(from, to, from.unitNumber);
        }
        private void GiveOrderToAttack(TerrainCell from, TerrainCell to, int unitsSent)
        {
            Unit attackingUnit = _playersUnits.GetUnit(from.owner);
            AttackCell attackCell = new(to, attackingUnit, unitsSent , DEFAULT_TIME_TO_ATTACK * attackingUnit.moveSpeed);
            from.unitNumber -= unitsSent;
            attackCell.OnAttackEnd += ChoseBattleSituation;
            attackCell.OnComandEnd += RemoveComand;
            _comandList.Add(attackCell);
            _orderDrawer.NewComand(from.transform.position, to.transform.position, attackCell);
        }

        public void RightSwipe(Vector3 swipeStartPosition, Vector3 swipeEndPosition)
        {
            if (IsConditionsCorrect(swipeStartPosition, swipeEndPosition))
            {
                TerrainCell from = _terrainTilemap.GetTile(swipeStartPosition);
                TerrainCell to = _terrainTilemap.GetTile(swipeEndPosition);
                TryGiveOrderToAttackHalfUnit(from, to);
            }
        }
        public void LeftSwipe(Vector3 swipeStartPosition, Vector3 swipeEndPosition)
        {
            if(IsConditionsCorrect(swipeStartPosition, swipeEndPosition)) 
            {
                TerrainCell from = _terrainTilemap.GetTile(swipeStartPosition);
                TerrainCell to = _terrainTilemap.GetTile(swipeEndPosition);
                TryGiveOrderToAttackAllUnit(from, to);
            }
        }

        private bool IsConditionsCorrect(TerrainCell from, TerrainCell to)
        {
            if (_terrainTilemap.isCellsNeighbours(to, from) && from.unitNumber >= 1)
            {
                return true;
            }
            return false;
        }
        private bool IsConditionsCorrect(Vector3 cellA, Vector3 cellB) 
        {
            if (_terrainTilemap.ContainTile(cellA)
                && _terrainTilemap.ContainTile(cellB))
            {
                TerrainCell from = _terrainTilemap.GetTile(cellA);
                TerrainCell to = _terrainTilemap.GetTile(cellB);
                return IsConditionsCorrect(from, to);
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

        private void IntatiateBattle(TerrainCell cell, int attackUnitCount, Unit attackUnit)
        {
            Unit defanceUnit = _playersUnits.GetUnit(cell.owner);

            int defanceUnitCount = cell.unitNumber;
            while(defanceUnitCount > 0 && attackUnitCount > 0) 
            {
                int mutalDefenderAttack = defanceUnit.attack * defanceUnitCount;
                int mutalAttackingAttack = attackUnit.attack * attackUnitCount;

                int killedDefenderUnits = (int)Mathf.Floor(mutalAttackingAttack / defanceUnit.defense);
                int killedAttackingUnits = (int)Mathf.Floor(mutalDefenderAttack / attackUnit.defense);

                attackUnitCount -= killedAttackingUnits;
                defanceUnitCount -= killedDefenderUnits;
            }

            if (attackUnitCount > 0)
            {
                cell.owner = attackUnit.owner;
                cell.unitNumber = attackUnitCount;
            }
            else 
            {
                cell.unitNumber = defanceUnitCount > 0? defanceUnitCount: 0;
            }
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

