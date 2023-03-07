using System.Collections.Generic;
using UnityEngine;
using TileSystem;
using EventBusSystem;

namespace BattleSystem 
{ 
    public class NestBuilder : IRegionControleOnePlayerHandler
    {
        private List<Region> _avalibleForNestBuilding = new();

        public NestBuilder() 
        {
            EventBus.Subscribe(this);
        }

        public void RegionControlOnePlayer(Region region, PlayersList owner)
        {
            if (!_avalibleForNestBuilding.Contains(region) && !region.isNestInRegion) 
            { 
                _avalibleForNestBuilding.Add(region);
            }
        }

        public bool TryBuildNest(TerrainCell cell) 
        {
            if (_avalibleForNestBuilding.Contains(cell.region)) 
            {
                BuildNest(cell);
                cell.region.isNestInRegion = true;
                return true;
            }
            return false;
        }  
        
        private void BuildNest(TerrainCell cell) 
        { 
            cell.isNestBuilt = true;
            _avalibleForNestBuilding.Remove(cell.region);
        }
    }
}

