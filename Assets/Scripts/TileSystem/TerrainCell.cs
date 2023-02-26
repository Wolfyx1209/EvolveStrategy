using UnityEngine;

namespace TileSystem
{
    public class TerrainCell : MonoBehaviour
    {
        private CellView view => 
            GetComponentInChildren<CellView>();
        public CellType cellType;

        private Region _region;
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
            set { _owner = value; }
        }
        public int unitNumber
        {
            get => _unitNumber;
            set 
            { 
                _unitNumber = value;
                UpdateUnitView();
            } 
        }
        
        public int foodNumber
        {
            get => _foodNumber;
            set
            {
                _foodNumber = value;
                UpdateFoodView();
            }
        }
        public bool isNestBuilt
        {
            get => _isNestBuilt;
            set
            {
                _isNestBuilt = value;
                UpdateNestView();
            }
        }

        private void Awake()
        {
            UpdateNestView();
            UpdateUnitView();
        }

        private SpriteRenderer _spriteRenderer => gameObject.GetComponent<SpriteRenderer>();

        public void ChangeColorTo(Color color) 
        { 
            _spriteRenderer.color = color;
        }
        private void UpdateUnitView() 
        {
            view.UpdateUnitView(_unitNumber, _owner);
        }

        private void UpdateNestView() 
        {
            view.UpdateNestView(_isNestBuilt);
        }

        private void UpdateFoodView() 
        { 
            
        }
    }
}

