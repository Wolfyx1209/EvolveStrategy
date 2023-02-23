using UnityEngine;

namespace TileSystem
{
    public class TerrainCell : MonoBehaviour
    {
        private CellView view => 
            GetComponentInChildren<CellView>();
        [SerializeField] public CellType cellType;

        [SerializeField] public Region region;
        [SerializeField] public PlayersList owner;
        [SerializeField] public int unitNumber;
        [SerializeField] public int foodNumber;
        [SerializeField] public bool isNestBuilt;

        private void Awake()
        {
            view.ChangeNestCondition(isNestBuilt);
            view.ChangeUnitView(unitNumber);
        }

        private SpriteRenderer _spriteRenderer => gameObject.GetComponent<SpriteRenderer>();

        public void ChangeColorTo(Color color) 
        { 
            _spriteRenderer.color = color;
        }
    }
}

