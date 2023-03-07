using System;
using System.Collections.Generic;
using System.Linq;
using TileSystem;
using UnityEngine;

public class HexMetrics
{
    public static float sideLength = 0.5f;
    public float radius = 0.43f; //sideLength * math.sin(60 * math.PI / 180);

    private TerrainTilemap _ttm;

    public HexMetrics()
    {
        _ttm = GameObject.FindObjectOfType<TerrainTilemap>();
    }

    public List<Vector3> GetCoordinatesOfCellVertex(Transform transform)
    {
        List<Vector3> coordinates = new List<Vector3>();
        var position = transform.position;

        coordinates.Add(GetVertex(transform, VertexDirection.leftTop));
        coordinates.Add(GetVertex(transform, VertexDirection.rightTop));
        coordinates.Add(GetVertex(transform, VertexDirection.right));
        coordinates.Add(GetVertex(transform, VertexDirection.rightBottom));
        coordinates.Add(GetVertex(transform, VertexDirection.leftBottom));
        coordinates.Add(GetVertex(transform, VertexDirection.left));
        //���� ���� ������ ����� �� ������� �������
        return coordinates;
    }

    public List<Vector3> FindCommonVertex(TerrainCell cellA, TerrainCell cellB)
    {
        return FindCommonVertex(GetCoordinatesOfCellVertex(cellA.transform), GetCoordinatesOfCellVertex(cellB.transform));
    }
    public List<Vector3> FindCommonVertex(List<Vector3> mainCellDots, List<Vector3> neighbouringCellDots)
    {
        var commonDots = mainCellDots.Intersect(neighbouringCellDots);
        return commonDots.ToList();
    }

    public List<Vector3> GetDotsCoordinatesForBorders(TerrainCell mainCell, TerrainCell neighbouringCell)
    {
        List<Vector3> dotsCoordinatesOfMainCell = GetCoordinatesOfCellVertex(mainCell.transform);
        List<Vector3> dotsCoordinatesOfNeighboursCell = GetCoordinatesOfCellVertex(neighbouringCell.transform);
        return FindCommonVertex(dotsCoordinatesOfMainCell, dotsCoordinatesOfNeighboursCell);


        //List<Vector2> dotsCoordinatesOfNeighbouringCell = _ttm.GetCellNeighbors()
    }

    public Vector3 GetVertex(Vector2Int coordinate, VertexDirection direction)
    {
        return (GetVertex(_ttm.GetCellFromGridCoordinate(coordinate).transform, direction));
    }

    public Vector3 GetVertex(Transform transform, VertexDirection direction)
    {
        Vector3 position = transform.transform.position;
        switch (direction)
        {
            case (VertexDirection.leftTop):
                return new Vector3(position.x - 0.5f * sideLength, position.y + radius, -1);
            case (VertexDirection.rightTop):
                return new Vector3(position.x + 0.5f * sideLength, position.y + radius, -1);
            case (VertexDirection.right):
                return new Vector3(position.x + sideLength, position.y, -1);
            case (VertexDirection.rightBottom):
                return new Vector3(position.x + 0.5f * sideLength, position.y - radius, -1);
            case (VertexDirection.leftBottom):
                return new Vector3(position.x - 0.5f * sideLength, position.y - radius, -1);
            case (VertexDirection.left):
                return new Vector3(position.x - sideLength, position.y, -1);
            default:
                throw new Exception("Incorrect input");
        }
    }

    public Vector2Int GetCellByDirection(Vector2Int coordinate, CellDirection direction)
    {
        return coordinate + GetCellNeiborModificator(direction, coordinate);
    }

    public List<Vector2Int> GetCoordinatesOfNeighboringCells(Vector2Int coordinate)
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

    public Vector2Int RotateCellClockwise(Vector2Int center, Vector2Int turned)
    {
        CellDirection displacement = GetRelativeCellPositionBRelativeToA(center, turned);
        if (displacement == CellDirection.rightTop) return GetCellByDirection(center, CellDirection.rightBottom);
        if (displacement == CellDirection.rightBottom) return GetCellByDirection(center, CellDirection.bottom);
        if (displacement == CellDirection.bottom) return GetCellByDirection(center, CellDirection.leftBottom);
        if (displacement == CellDirection.leftBottom) return GetCellByDirection(center, CellDirection.leftTop);
        if (displacement == CellDirection.leftTop) return GetCellByDirection(center, CellDirection.top);
        if (displacement == CellDirection.top) return GetCellByDirection(center, CellDirection.rightTop);

        return GetCellByDirection(center, CellDirection.rightBottom);
    }

    public Vector2Int RotateCellCounterClockwise(Vector2Int center, Vector2Int turned)
    {
        CellDirection displacement = GetRelativeCellPositionBRelativeToA(center, turned);
        if (displacement == CellDirection.rightTop) return GetCellByDirection(center, CellDirection.top);
        if (displacement == CellDirection.rightBottom) return GetCellByDirection(center, CellDirection.rightTop);
        if (displacement == CellDirection.bottom) return GetCellByDirection(center, CellDirection.rightBottom);
        if (displacement == CellDirection.leftBottom) return GetCellByDirection(center, CellDirection.bottom);
        if (displacement == CellDirection.leftTop) return GetCellByDirection(center, CellDirection.leftBottom);
        if (displacement == CellDirection.top) return GetCellByDirection(center, CellDirection.leftTop);

        return GetCellByDirection(center, CellDirection.rightBottom);
    }

    public CellDirection GetRelativeCellPositionBRelativeToA(Vector2Int a, Vector2Int b)
    {
        Vector2Int displacement = b - a;

        // Define the six possible relative positions
        Vector2Int rightTop = GetCellNeiborModificator(CellDirection.rightTop, a);
        Vector2Int rightBottom = GetCellNeiborModificator(CellDirection.rightBottom, a);
        Vector2Int bottom = GetCellNeiborModificator(CellDirection.bottom, a);
        Vector2Int leftBottom = GetCellNeiborModificator(CellDirection.leftBottom, a);
        Vector2Int leftTop = GetCellNeiborModificator(CellDirection.leftTop, a);
        Vector2Int top = GetCellNeiborModificator(CellDirection.top, a);

        // Check which relative position matches the displacement vector, and return it
        if (displacement == rightTop) return CellDirection.rightTop;
        if (displacement == rightBottom) return CellDirection.rightBottom;
        if (displacement == bottom) return CellDirection.bottom;
        if (displacement == leftBottom) return CellDirection.leftBottom;
        if (displacement == leftTop) return CellDirection.leftTop;
        if (displacement == top) return CellDirection.top;

        throw new Exception("Incorrect Input");
    }

    public Vector2Int GetCellNeiborModificator(CellDirection direction, Vector2Int coordinate)
    {
        int x = 0;
        int y = 0;

        switch (direction)
        {
            case CellDirection.top:
                x++;
                break;
            case CellDirection.rightTop:
                if (coordinate.y % 2 != 0)
                    x++;
                y++;
                break;
            case CellDirection.rightBottom:
                x += (coordinate.y % 2 == 0) ? -1 : 0;
                y++;
                break;
            case CellDirection.bottom:
                x--;
                break;
            case CellDirection.leftBottom:
                x += (coordinate.y % 2 == 0) ? -1 : 0;
                y--;
                break;
            case CellDirection.leftTop:
                if (coordinate.y % 2 != 0)
                {
                    x++;
                }
                y--;
                break;
        }

        return new Vector2Int(x, y);
    }

    public Vector3 GetRightBottomVertex(Vector2Int a, Vector2Int b)
    {
        CellDirection displacement = GetRelativeCellPositionBRelativeToA(a, b);
        if (displacement == CellDirection.rightTop) return GetVertex(a, VertexDirection.right);
        if (displacement == CellDirection.rightBottom) return GetVertex(a, VertexDirection.rightBottom);
        if (displacement == CellDirection.bottom) return GetVertex(a, VertexDirection.leftBottom);
        if (displacement == CellDirection.leftBottom) return GetVertex(a, VertexDirection.left);
        if (displacement == CellDirection.leftTop) return GetVertex(a, VertexDirection.leftTop);
        if (displacement == CellDirection.top) return GetVertex(a, VertexDirection.rightTop);
        throw new Exception("Incorrect input");
    }
}

