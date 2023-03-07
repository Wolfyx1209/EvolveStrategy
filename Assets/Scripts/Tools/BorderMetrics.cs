using System.Collections.Generic;
using TileSystem;
using UnityEngine;

public class BorderMetrics
{
    private TerrainTilemap _tilemap;
    private Dictionary<Vector2Int, TerrainCell> _cells = new();
    private HexMetrics hexMetrics = new();
    public BorderMetrics()
    {
        _tilemap = GameObject.FindObjectOfType<TerrainTilemap>();
    }
    public List<Vector3> GetRegionBorder(List<TerrainCell> regionCells)
    {
        List<Vector3> perim = new();
        Vector2Int footCell = (Vector2Int)_tilemap.GetCellCoordinate(regionCells[0]);
        WriteCellToDictionary(regionCells);

        while (_cells.ContainsKey(GetCoordinatesOfNeighboringCells(footCell)[4]))
        {
            footCell = GetCoordinatesOfNeighboringCells(footCell)[4];
        }

        Vector2Int startHandCell = GetCoordinatesOfNeighboringCells(footCell)[4];
        Vector2Int startFootCell = footCell;
        Vector2Int handCell = startHandCell;

        perim.Add(hexMetrics.GetRightBottomVertex(footCell, handCell));

        do
        {
            if (_cells.ContainsKey(hexMetrics.RotateCellCounterClockwise(handCell, footCell)))
            {
                footCell = hexMetrics.RotateCellCounterClockwise(handCell, footCell);
            }
            else
            {
                handCell = hexMetrics.RotateCellClockwise(footCell, handCell);
            }
            perim.Add(hexMetrics.GetRightBottomVertex(footCell, handCell));
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
    private List<Vector2Int> GetCoordinatesOfNeighboringCells(Vector2Int coordinate)
    {
        List<Vector2Int> coordinates = new();
        coordinates.Add(new Vector2Int(coordinate.x - 1, coordinate.y));
        coordinates.Add(new Vector2Int(coordinate.x + 1, coordinate.y));
        coordinates.Add(new Vector2Int(coordinate.x, coordinate.y - 1));
        coordinates.Add(new Vector2Int(coordinate.x, coordinate.y + 1));
        if (coordinate.y % 2 == 0)
        {
            coordinates.Add(new Vector2Int(coordinate.x - 1, coordinate.y + 1));
            coordinates.Add(new Vector2Int(coordinate.x - 1, coordinate.y - 1));
        }
        else
        {
            coordinates.Add(new Vector2Int(coordinate.x + 1, coordinate.y + 1));
            coordinates.Add(new Vector2Int(coordinate.x + 1, coordinate.y - 1));
        }
        return coordinates;
    }
}
