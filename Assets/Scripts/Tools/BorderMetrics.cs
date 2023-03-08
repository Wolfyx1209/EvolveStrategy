using System.Collections.Generic;
using TileSystem;
using UnityEngine;

public class BorderMetrics
{
    private TerrainTilemap _tilemap;
    private Dictionary<Vector2Int, TerrainCell> _cells = new();
    public BorderMetrics()
    {
        _tilemap = GameObject.FindObjectOfType<TerrainTilemap>();
    }
    public List<Vector3> GetRegionBorder(List<TerrainCell> regionCells)
    {
        List<Vector3> perim = new();
        Vector2Int footCell = (Vector2Int)_tilemap.GetCellCoordinate(regionCells[0]);
        Vector2Int handCell = HexMetrics.GetCellByDirection(footCell, CellDirection.top);
        WriteCellToDictionary(regionCells);

        while (_cells.ContainsKey(handCell))
        {
            footCell = handCell;
            handCell = HexMetrics.GetCellByDirection(footCell, CellDirection.top);
        }

        Vector2Int startHandCell = handCell;
        Vector2Int startFootCell = footCell;

        perim.Add(HexMetrics.GetRightBottomVertex(footCell, handCell, _cells[footCell].transform));

        int n = 0;
        do
        {
            n++;
            if (_cells.ContainsKey(HexMetrics.RotateCellCounterClockwise(handCell, footCell)))
            {
                footCell = HexMetrics.RotateCellCounterClockwise(handCell, footCell);
            }
            else
            {
                handCell = HexMetrics.RotateCellClockwise(footCell, handCell);
            }
            perim.Add(HexMetrics.GetRightBottomVertex(footCell, handCell, _cells[footCell].transform));
            if (n > 100) 
            {
                Debug.Log("InfinitySycle");
                break;
            }
        } while ((handCell != startHandCell || footCell != startFootCell));
        return perim;
    }

    private void WriteCellToDictionary(List<TerrainCell> cells)
    {
        foreach (TerrainCell cell in cells)
        {
            this._cells.Add((Vector2Int)_tilemap.GetCellCoordinate(cell), cell);
        }
    }
}
