using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

namespace TileSystem
{
    public class Region
    {
        private List<TerrainCell> _regionCells = new();

        public bool isNestInRegion;

        private GameObject _regionView;
        private NestBuildView _buildView;

        private Dictionary<PlayersList, SubRegionView> _views = new();

        public bool isFade = true;

        public Region() 
        {
            _regionView = new GameObject("RegionView");
            _regionView.transform.SetParent(GameObject.FindGameObjectWithTag("GUICanvas").transform, false);
            _regionView.AddComponent<RectTransform>();
        }

        public void AddCell(TerrainCell cell) 
        {
            if (!_regionCells.Contains(cell)) 
            { 
                _regionCells.Add(cell);
                isNestInRegion |= cell.isNestBuilt;  
                FindSubRegionForCell(cell);
            }
        }

        [System.Obsolete]
        public void DrawRegionBoundes() 
        {
            //Color color = Random.ColorHSV();
            //foreach(TerrainCell cell in _regionCells) 
            //{
            //    cell.ChangeColorTo(color);
            //}
            //List<Vector3> vector3s = hm.GetCoordinatesOfCell();

            BorderMetrics bm = new();
            List<Vector3> vector3s = bm.GetRegionBorder(_regionCells);
            GameObject go = (GameObject)Object.Instantiate(Resources.Load("ViewElements/RegionBorder"));
            go.transform.SetParent(_regionView.transform);
            LineRenderer lr = go.GetComponent<LineRenderer>();
            lr.numPositions = vector3s.Count;
            lr.SetPositions(vector3s.ToArray());
        }

        public void ShowCellsInfo() 
        {
            foreach (KeyValuePair<PlayersList, SubRegionView> pair in _views)
            {
                pair.Value.ShowCellsInfo();
            }
            isFade = false;
        }

        public void HideCellsInfo() 
        {
            foreach(KeyValuePair<PlayersList, SubRegionView> pair in _views) 
            {
                pair.Value.HideCellsInfo();
            }
            isFade = true;
        }

        private SubRegionView CreateNewSubViewElement() 
        {
            GameObject newViewObject = Object.Instantiate(Resources.Load<GameObject>("ViewElements/SubRegionView"));
            newViewObject.transform.SetParent(_regionView.transform, false);
            SubRegionView view = newViewObject.GetComponent<SubRegionView>();
            view.OnCellChangeOwner += FindSubRegionForCell;
            view.OnCellChangeOwner += NotifyIfRegionContloledOnePlayer;
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
                _views.Add(cell.owner, CreateNewSubViewElement());
                _views[cell.owner].AddCell(cell);
            }
        }
        private void NotifyIfRegionContloledOnePlayer(TerrainCell cell) 
        { 
            if(IsOnePlayerControlRegion() && !isNestInRegion) 
            {
                EventBus.RaiseEvent<IRegionControleOnePlayerHandler>(it => it.RegionControlOnePlayer(this, cell.owner));
                if(cell.owner == PlayersList.Player) 
                {
                    ShowNestBuildingViewForPlayer();
                }
            }
        }
        private bool IsOnePlayerControlRegion() 
        {
            PlayersList owner = _regionCells[0].owner;
            foreach(TerrainCell cell in _regionCells) 
            { 
                if(cell.owner != owner) 
                {
                    return false;
                }
            }
            return true;
        }
        private void ShowNestBuildingViewForPlayer()
        {
            GameObject viewPrefab = (GameObject)Resources.Load("ViewElements/BuildNestIcon");
            GameObject viewObject = Object.Instantiate(viewPrefab, _regionView.transform);
            viewObject.transform.position = CalculateCenter();
            _buildView = viewObject.GetComponent<NestBuildView>();
            _buildView.OnClick += PlayerClickedOnNestBuildButton;

        }

        private void PlayerClickedOnNestBuildButton() 
        {
            EventBus.RaiseEvent<IPlayerChoosesNestCellHandler>(it => it.StartState(this));
            _buildView.OnClick -= PlayerClickedOnNestBuildButton;
            Object.Destroy(_buildView.gameObject);
        }

        private Vector3 CalculateCenter()
        {
            Vector3 center = new();
            foreach (TerrainCell cell in _regionCells)
            {
                center += cell.transform.position;
            }
            return center / _regionCells.Count;
        }
    }
}

