using System.Collections.Generic;
using UnityEngine;

namespace TileSystem
{
    public class Region
    {
        public List<TerrainCell> _regionCells = new();

        public bool isNestInRegion;

        public void DrawRegionBoundes() 
        {
            Color color = Random.ColorHSV();
            foreach(TerrainCell cell in _regionCells) 
            {
                cell.ChangeColorTo(color);
            }
        }
    }
}

