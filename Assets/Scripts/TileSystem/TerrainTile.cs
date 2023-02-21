using UnityEngine;

namespace TileSystem
{
    public class TerrainTile : MonoBehaviour
    {
        [SerializeField] private CellInfo _cellInfo;

        [SerializeField] public PlayersList owner;
        [SerializeField] public int unitNumber;
        [SerializeField] public int foodNumber;
        [SerializeField] public bool isNestBuilt;

        private void Start()
        {
            TerrainTilemap tilemap = GetComponentInParent<TerrainTilemap>();
            tilemap.AddTerrainTile(transform.position, this);
        }

        private SpriteRenderer _spriteRenderer => gameObject.GetComponent<SpriteRenderer>();
        public CellInfo GetCellInfo() => _cellInfo;

        public void ChangeColorTo(Color color) 
        { 
            _spriteRenderer.color = color;
        }
    }
}

