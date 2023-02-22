using UnityEngine;

namespace TileSystem
{
    public class TerrainCell : MonoBehaviour
    {
        [SerializeField] public CellType cellType;

        [SerializeField] public Region region;
        [SerializeField] public PlayersList owner;
        [SerializeField] public int unitNumber;
        [SerializeField] public int foodNumber;
        [SerializeField] public bool isNestBuilt;

        private SpriteRenderer _spriteRenderer => gameObject.GetComponent<SpriteRenderer>();

        public void ChangeColorTo(Color color) 
        { 
            _spriteRenderer.color = color;
        }
    }
}

