using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

namespace TileSystem
{
    public class Region
    {
        private List<TerrainCell> _regionCells = new();

        public bool isNestInRegion;
        private bool isRegionControledOnePlayer;

        private GameObject _regionView;
        private LineRenderer _regionBoundes;
        private NestBuildView _buildView;

        private Dictionary<GameAcktor, SubRegionView> _views = new();

        public bool isFade = true;

        public Region(List<TerrainCell> regionCells) 
        {
            _regionCells = regionCells;
            _regionView = new GameObject("RegionView");
            _regionView.transform.SetParent(GameObject.FindGameObjectWithTag("GUICanvas").transform, false);
            _regionView.AddComponent<RectTransform>();
            foreach(TerrainCell cell in _regionCells) 
            {
                cell.region = this;
                isNestInRegion |= cell.isNestBuilt;
                FindSubRegionForCell(cell);
            }
            DrawRegionBoundes();
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

        private void DrawRegionBoundes() 
        {
            List<Vector3> vector3s = new BorderMetrics().GetRegionBorder(_regionCells);
            GameObject go = (GameObject)Object.Instantiate(Resources.Load("ViewElements/RegionBorder"));
            go.transform.SetParent(_regionView.transform);
            _regionBoundes = go.GetComponent<LineRenderer>();
            _regionBoundes.positionCount = vector3s.Count;
            _regionBoundes.SetPositions(vector3s.ToArray());
        }

        public void ShowCellsInfo() 
        {
            foreach (KeyValuePair<GameAcktor, SubRegionView> pair in _views)
            {
                pair.Value.ShowCellsInfo();
            }
            _regionBoundes.enabled = true;
            isFade = false;
        }

        public void HideCellsInfo() 
        {
            foreach(KeyValuePair<GameAcktor, SubRegionView> pair in _views) 
            {
                pair.Value.HideCellsInfo();
            }
            _regionBoundes.enabled = false;
            isFade = true;
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

        private SubRegionView CreateNewSubViewElement()
        {
            GameObject newViewObject = Object.Instantiate(Resources.Load<GameObject>("ViewElements/SubRegionView"));
            newViewObject.transform.SetParent(_regionView.transform, false);
            SubRegionView view = newViewObject.GetComponent<SubRegionView>();
            view.OnCellChangeOwner += FindSubRegionForCell;
            view.OnCellChangeOwner += NotifyIfRegionControlStatusChanged;
            view.OnEmptyView += DeleteEmptySubViewElement;
            return view;
        }

        private void DeleteEmptySubViewElement(GameAcktor owner)
        {
            Object.Destroy(_views[owner].gameObject);
            _views.Remove(owner);
        }

        private void NotifyIfRegionControlStatusChanged(TerrainCell cell) 
        { 
            if(IsOnePlayerControlRegion()) 
            {
                EventBus.RaiseEvent<IRegionOwnershipStatusChangedHandler>(it => it.RegionControledBySinglePlayer(this, cell.owner));
                if(cell.owner.acktorName == PlayersList.Player && !isNestInRegion) 
                {
                    ShowNestBuildingViewForPlayer();
                }
            }
            else if (isRegionControledOnePlayer) 
            {
                HideNestBuildingViewForPlayer();
            }
        }
        private bool IsOnePlayerControlRegion() 
        {
            GameAcktor owner = _regionCells[0].owner;
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

        private void HideNestBuildingViewForPlayer()
        {
            EventBus.RaiseEvent<IPlayerChoosesNestCellHandler>(it => it.EndState(this));
            Object.Destroy(_buildView);
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

