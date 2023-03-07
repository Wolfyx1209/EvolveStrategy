using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMetrixTest : MonoBehaviour
{
    private HexMetrics hm;

    private void Start()
    {
        hm = new HexMetrics();
        if (hm.GetCellByDirection(new Vector2Int(0, 0), CellDirection.leftTop) != new Vector2Int(0, -1)) throw new System.Exception("1");
        if (hm.GetCellByDirection(new Vector2Int(0, 0), CellDirection.top) != new Vector2Int(1, 0)) throw new System.Exception("2");
        if (hm.GetCellByDirection(new Vector2Int(0, 0), CellDirection.rightTop) != new Vector2Int(0, 1)) throw new System.Exception("3");
        if (hm.GetCellByDirection(new Vector2Int(0, 0), CellDirection.rightBottom) != new Vector2Int(-1, 1)) throw new System.Exception("4");
        if (hm.GetCellByDirection(new Vector2Int(0, 0), CellDirection.bottom) != new Vector2Int(-1, 0)) throw new System.Exception("5");
        if (hm.GetCellByDirection(new Vector2Int(0, 0), CellDirection.leftBottom) != new Vector2Int(-1, -1)) throw new System.Exception("6");

        if (hm.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.leftTop) != new Vector2Int(0,0)) throw new System.Exception("7" + hm.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.leftTop));
        if (hm.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.top) != new Vector2Int(0,1)) throw new System.Exception("8");
        if (hm.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.rightTop) != new Vector2Int(0,2)) throw new System.Exception("9");
        if (hm.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.rightBottom) != new Vector2Int(-1, 2)) throw new System.Exception("10");
        if (hm.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.bottom) != new Vector2Int(-2, 1)) throw new System.Exception("11");
        if (hm.GetCellByDirection(new Vector2Int(-1, 1), CellDirection.leftBottom) != new Vector2Int(-1, 0)) throw new System.Exception("12");

        if (hm.GetRelativeCellPositionBRelativeToA(new Vector2Int(-1, 1), new Vector2Int(0, 1)) != CellDirection.top) throw new System.Exception("13");
        if (hm.GetRelativeCellPositionBRelativeToA(new Vector2Int(-1, 1), new Vector2Int(0, 2)) != CellDirection.rightTop) throw new System.Exception("14");
        if (hm.GetRelativeCellPositionBRelativeToA(new Vector2Int(-1, 1), new Vector2Int(-1, 2)) != CellDirection.rightBottom) throw new System.Exception("15");
        if (hm.GetRelativeCellPositionBRelativeToA(new Vector2Int(-1, 1), new Vector2Int(-2, 1)) != CellDirection.bottom) throw new System.Exception("16");
        if (hm.GetRelativeCellPositionBRelativeToA(new Vector2Int(-1, 1), new Vector2Int(-1, 0)) != CellDirection.leftBottom) throw new System.Exception("17");
        if (hm.GetRelativeCellPositionBRelativeToA(new Vector2Int(-1, 1), new Vector2Int(0, 0)) != CellDirection.leftTop) throw new System.Exception("18");

        if (hm.RotateCellClockwise(new Vector2Int(-1, 1), new Vector2Int(0, 1)) != new Vector2Int(0,2)) throw new System.Exception("13");
        if (hm.RotateCellClockwise(new Vector2Int(-1, 1), new Vector2Int(0, 2)) != new Vector2Int(-1, 2)) throw new System.Exception("14");
        if (hm.RotateCellClockwise(new Vector2Int(-1, 1), new Vector2Int(-1, 2)) != new Vector2Int(-2, 1)) throw new System.Exception("15");
        if (hm.RotateCellClockwise(new Vector2Int(-1, 1), new Vector2Int(-2, 1)) != new Vector2Int(-1 ,0)) throw new System.Exception("16");
        if (hm.RotateCellClockwise(new Vector2Int(-1, 1), new Vector2Int(-1, 0)) != new Vector2Int(0,0)) throw new System.Exception("17");
        if (hm.RotateCellClockwise(new Vector2Int(-1, 1), new Vector2Int(0, 0)) != new Vector2Int(0,1)) throw new System.Exception("18");
    }
}
