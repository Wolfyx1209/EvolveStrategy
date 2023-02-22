using TileSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CellFader : MonoBehaviour
{
    private GameObject _previousTile;
    [SerializeField] private TerrainTilemap _terrainTilemap;
    private void FixedUpdate()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (_terrainTilemap.ContainTile(cursorPosition)) 
        {
            GameObject tile = null;
            tile = _terrainTilemap.GetTile(cursorPosition).gameObject;
            if (tile != null && tile != _previousTile)
            {
                if(_previousTile != null)
                    _previousTile.GetComponent<TerrainCell>().ChangeColorTo(Color.white);
                tile.GetComponent<TerrainCell>().ChangeColorTo(Color.gray);
                _previousTile = tile;
            }
        }
    }
}
