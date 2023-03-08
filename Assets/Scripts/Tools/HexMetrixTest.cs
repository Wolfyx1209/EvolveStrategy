using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMetrixTest : MonoBehaviour
{

    private void Start()
    {
        if (HexMetrics.GetCellByDirection(new Vector2Int(0, 0), CellDirection.leftTop) != new Vector2Int(0, -1)) throw new System.Exception("1");
        if (HexMetrics.GetCellByDirection(new Vector2Int(0, 0), CellDirection.top) != new Vector2Int(1, 0)) throw new System.Exception("2");
        if (HexMetrics.GetCellByDirection(new Vector2Int(0, 0), CellDirection.rightTop) != new Vector2Int(0, 1)) throw new System.Exception("3");
        if (HexMetrics.GetCellByDirection(new Vector2Int(0, 0), CellDirection.rightBottom) != new Vector2Int(-1, 1)) throw new System.Exception("4");
        if (HexMetrics.GetCellByDirection(new Vector2Int(0, 0), CellDirection.bottom) != new Vector2Int(-1, 0)) throw new System.Exception("5");
        if (HexMetrics.GetCellByDirection(new Vector2Int(0, 0), CellDirection.leftBottom) != new Vector2Int(-1, -1)) throw new System.Exception("6");

        if (HexMetrics.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.leftTop) != new Vector2Int(0,0)) throw new System.Exception("7");
        if (HexMetrics.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.top) != new Vector2Int(0,1)) throw new System.Exception("8");
        if (HexMetrics.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.rightTop) != new Vector2Int(0,2)) throw new System.Exception("9");
        if (HexMetrics.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.rightBottom) != new Vector2Int(-1, 2)) throw new System.Exception("10");
        if (HexMetrics.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.bottom) != new Vector2Int(-2, 1)) throw new System.Exception("11");
        if (HexMetrics.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.leftBottom) != new Vector2Int(-1, 0)) throw new System.Exception("12");

        if (HexMetrics.GetRelativeCellPositionBRelativeToA(new Vector2Int(-1, 1), new Vector2Int(0, 1)) != CellDirection.top) throw new System.Exception("13");
        if (HexMetrics.GetRelativeCellPositionBRelativeToA(new Vector2Int(-1, 1), new Vector2Int(0, 2)) != CellDirection.rightTop) throw new System.Exception("14");
        if (HexMetrics.GetRelativeCellPositionBRelativeToA(new Vector2Int(-1, 1), new Vector2Int(-1, 2)) != CellDirection.rightBottom) throw new System.Exception("15");
        if (HexMetrics.GetRelativeCellPositionBRelativeToA(new Vector2Int(-1, 1), new Vector2Int(-2, 1)) != CellDirection.bottom) throw new System.Exception("16");
        if (HexMetrics.GetRelativeCellPositionBRelativeToA(new Vector2Int(-1, 1), new Vector2Int(-1, 0)) != CellDirection.leftBottom) throw new System.Exception("17");
        if (HexMetrics.GetRelativeCellPositionBRelativeToA(new Vector2Int(-1, 1), new Vector2Int(0, 0)) != CellDirection.leftTop) throw new System.Exception("18");

        if (HexMetrics.RotateCellClockwise(new Vector2Int(-1, 1), new Vector2Int(0, 1)) != new Vector2Int(0,2)) throw new System.Exception("13");
        if (HexMetrics.RotateCellClockwise(new Vector2Int(-1, 1), new Vector2Int(0, 2)) != new Vector2Int(-1, 2)) throw new System.Exception("14");
        if (HexMetrics.RotateCellClockwise(new Vector2Int(-1, 1), new Vector2Int(-1, 2)) != new Vector2Int(-2, 1)) throw new System.Exception("15");
        if (HexMetrics.RotateCellClockwise(new Vector2Int(-1, 1), new Vector2Int(-2, 1)) != new Vector2Int(-1 ,0)) throw new System.Exception("16");
        if (HexMetrics.RotateCellClockwise(new Vector2Int(-1, 1), new Vector2Int(-1, 0)) != new Vector2Int(0,0)) throw new System.Exception("17");
        if (HexMetrics.RotateCellClockwise(new Vector2Int(-1, 1), new Vector2Int(0, 0)) != new Vector2Int(0,1)) throw new System.Exception("18");
    }
}
