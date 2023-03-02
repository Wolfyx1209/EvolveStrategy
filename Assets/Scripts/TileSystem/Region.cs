using System.Collections.Generic;
using UnityEngine;

namespace TileSystem
{
    public class Region
    {
        private List<TerrainCell> _regionCells = new();

        public bool isNestInRegion;

        private Dictionary<PlayersList, RegionView> _views = new();

        public bool isFade = true;

        public void AddCell(TerrainCell cell) 
        {
            if (!_regionCells.Contains(cell)) 
            { 
                _regionCells.Add(cell);
                FindSubRegionForCell(cell);
            }
        }

        public void DrawRegionBoundes() 
        {
            Color color = Random.ColorHSV();
            foreach(TerrainCell cell in _regionCells) 
            {
                cell.ChangeColorTo(color);
            }
        }

        public void ShowCellsInfo() 
        {
            foreach (KeyValuePair<PlayersList, RegionView> pair in _views)
            {
                pair.Value.ShowCellsInfo();
            }
            isFade = false;
        }

        public void HideCellsInfo() 
        {
            foreach(KeyValuePair<PlayersList, RegionView> pair in _views) 
            {
                pair.Value.HideCellsInfo();
            }
            isFade = true;
        }

        private RegionView CreateNewViewElement() 
        {
            GameObject newViewObject = Object.Instantiate(Resources.Load<GameObject>("ViewElements/RegionView"));
            newViewObject.transform.SetParent(GameObject.FindGameObjectWithTag("GUICanvas").transform, false);
            RegionView view = newViewObject.GetComponent<RegionView>();
            view.OnCellChangeOwner += FindSubRegionForCell;
            return view;
        }

        private void FindSubRegionForCell(TerrainCell cell) 
        {
            if (_views.ContainsKey(cell.owner))
            {
                _views[cell.owner].AddCell(cell);
            }
            else
            {
                _views.Add(cell.owner, CreateNewViewElement());
                _views[cell.owner].AddCell(cell);
            }
        }
    }
}

