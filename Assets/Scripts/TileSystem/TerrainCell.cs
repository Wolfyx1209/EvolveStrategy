using EventBusSystem;
using UnityEngine;

namespace TileSystem
{
    public class TerrainCell : MonoBehaviour
    {
        #region Events
        public delegate void OwnerChenge(PlayersList previousOwner, PlayersList newOwner, TerrainCell cell);
        public event OwnerChenge OnOwnerChenge;

        public delegate void UnitNumberChenge(int previousNumber, int newNumber, TerrainCell cell);
        public event UnitNumberChenge OnUnitNumberChenge;

        public delegate void NestConditionChenge(bool preciousState, bool newState, TerrainCell cell);
        public event NestConditionChenge OnNestConditionChenge;

        public delegate void FoodNumberChenge(int previousNumber, int newNumber, TerrainCell cell);
        public event UnitNumberChenge OnFoodNumberChenge;
        #endregion
        private CellView view => 
            GetComponentInChildren<CellView>();
        public CellType cellType;

        private Region _region = null;

        private bool _isShowen; 
        private Nest _nest;
        [SerializeField] private PlayersList _owner;
        [SerializeField] private int _unitNumber;
        [SerializeField] private int _foodNumber;
        [SerializeField] private bool _isNestBuilt; 

        public Region region 
        { 
            get => _region;
            set { _region = value; } 
        }
        public PlayersList owner
        {
            get => _owner;
            set 
            {
                PlayersList previousOwner = _owner;
                _owner = value;
                EventBus.RaiseEvent<ICellChangeOwnerHandler>(it => it.ChangeOwner(previousOwner, value, this));
                OnOwnerChenge?.Invoke(previousOwner, value, this);
            }
        }
        public int unitNumber
        {
            get => _unitNumber;
            set 
            { 
                OnUnitNumberChenge?.Invoke(_unitNumber, value, this);
                _unitNumber = value;
                UpdateUnitView();
            } 
        }
        
        public int foodNumber
        {
            get => _foodNumber;
            set
            {
                OnFoodNumberChenge?.Invoke(_foodNumber, value, this);
                _foodNumber = value;
                UpdateFoodView();
            }
        }
        public bool isNestBuilt
        {
            get => _isNestBuilt;
            set
            {
                OnNestConditionChenge.Invoke(_isNestBuilt, value, this);
                _isNestBuilt = value;
                UpdateNestView();
                BuildOrDestroyNest();
            }
        }

        private void Awake()
        {
            UpdateNestView();
            UpdateUnitView();
            BuildOrDestroyNest();
        }

        private SpriteRenderer _spriteRenderer => gameObject.GetComponent<SpriteRenderer>();

        public void ChangeColorTo(Color color) 
        { 
            _spriteRenderer.color = color;
        }

        public void HideView() 
        {
            _isShowen = false;
            view.HideView();
        }

        public void ShowView() 
        {
            _isShowen = true;
            view.ShowView(_isNestBuilt);
        }
        private void UpdateUnitView() 
        {
            view.UpdateUnitView(_unitNumber, _owner, _isShowen);
        }

        private void UpdateNestView() 
        {
            view.UpdateNestView(_isNestBuilt, _isShowen);
        }

        private void UpdateFoodView() 
        { 
            
        }

        private void BuildOrDestroyNest() 
        {
            if (isNestBuilt)
            {
                _nest = new Nest(this);
            }
            else
            {
                _nest = null;
            }
        }
    }
}

