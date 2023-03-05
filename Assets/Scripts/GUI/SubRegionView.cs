using System.Collections.Generic;
using TileSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubRegionView : MonoBehaviour
{
    public delegate void CellChangeOwner(TerrainCell cell);
    public event CellChangeOwner OnCellChangeOwner;

    private TextMeshProUGUI _unitsNumberTxt =>
        GetComponentInChildren<TextMeshProUGUI>();

    private Image _nestIcon =>
        GetComponentInChildren<Image>();

    private List<TerrainCell> cells = new();

    [SerializeField]private int _unitNumber;
    private int _foodNumber;
    private bool _isNestBuilt;
    private PlayersList _owner;
    private int unitNumber
    {
        get => _unitNumber;
        set { _unitNumber = value; 
            UpdateUnitView(); }
    }

    private int foodNumber
    {
        get => _foodNumber;
        set { _foodNumber = value; 
            UpdateFoodView(); }
    }

    private bool isNestBuilt
    {
        get => _isNestBuilt;
        set { _isNestBuilt = value; 
            UpdateNestView(); }
    }

    private PlayersList owner
    {
        get => _owner;
        set { _owner = value;
            UpdateOwnerView(); }
    }


    private void Awake()
    {
        transform.transform.localScale =
            gameObject.GetComponentInParent<Transform>().localScale;
    }
    private void OnEnable()
    {
        foreach (TerrainCell cell in cells)
        {
            SubscribeToAllChangeIvennts(cell);
        }
    }
    private void OnDisable()
    {
        foreach (TerrainCell cell in cells)
        {
            UnSubscribeToAllChangeIvennts(cell);
        }
    }
    private void OnDestroy()
    {
        foreach (TerrainCell cell in cells)
        {
            UnSubscribeToAllChangeIvennts(cell);
        }
        cells.Clear();
    }

    public void ShowCellsInfo()
    {
        if (cells.Count != 0)
        {
            foreach (TerrainCell cell in cells)
            {
                cell.ShowView();
            }
            HideGeneralInfo();
        }
    }

    public void HideCellsInfo()
    {
        if (cells.Count != 0)
        {
            foreach (TerrainCell cell in cells)
            {
                cell.HideView();
            }
            ShowGeneralInfo();
        }
    }

    public void AddCell(TerrainCell cell)
    {
        if (!cells.Contains(cell))
        {
            SubscribeToAllChangeIvennts(cell);
            cells.Add(cell);
            if (cells.Count == 0)
            {
                owner = cell.owner;
            }
            unitNumber += cell.unitNumber;
            foodNumber += cell.foodNumber;
            isNestBuilt |= cell.isNestBuilt;
            transform.position = CalculateCenter();
        }
    }

    public void RemoveCell(TerrainCell cell)
    {
        UnSubscribeToAllChangeIvennts(cell);
        unitNumber -= cell.unitNumber;
        foodNumber -= cell.foodNumber;
        isNestBuilt &= !cell.isNestBuilt;
        cells.Remove(cell);
    }

    private void HideGeneralInfo() 
    {
        Color col = _unitsNumberTxt.faceColor;
        col.a = 0;
        _unitsNumberTxt.faceColor = col;
    }
    private void ShowGeneralInfo() 
    {
        Color col = _unitsNumberTxt.faceColor;
        col.a = 1;
        _unitsNumberTxt.faceColor = col;
    }

    private void UpdateUnitView()
    {
        _unitsNumberTxt.text = unitNumber.ToString();
        Color ownerColor = new PlayersColors().GetColor(cells[0].owner);
        _unitsNumberTxt.faceColor = ownerColor;
    }

    private void UpdateNestView()
    {
        Color col = _nestIcon.color;
        col.a = isNestBuilt ? 1 : 0;
        _nestIcon.color = col;
    }

    private void UpdateFoodView() 
    {

    }

    private void UpdateOwnerView()
    {
        Color col = _nestIcon.color;
        col.a = isNestBuilt ? 1 : 0;
        _nestIcon.color = col;
    }

    private Vector3 CalculateCenter()
    {
        Vector3 center = new();
        foreach (TerrainCell cell in cells)
        {
            center += cell.transform.position;
        }
        return center / cells.Count;
    }

    private void SubscribeToAllChangeIvennts(TerrainCell cell)
    {
        cell.OnOwnerChenge += ChangeOwner;
        cell.OnUnitNumberChenge += ChangeUnitNumber;
        cell.OnNestConditionChenge += ChangeNestCondition;
        cell.OnFoodNumberChenge += ChangeFoodNumber;
    }

    private void UnSubscribeToAllChangeIvennts(TerrainCell cell)
    {
        cell.OnOwnerChenge -= ChangeOwner;
        cell.OnUnitNumberChenge -= ChangeUnitNumber;
        cell.OnNestConditionChenge -= ChangeNestCondition;
        cell.OnFoodNumberChenge -= ChangeFoodNumber;
    }
    private void ChangeOwner(PlayersList previousOwner, PlayersList newOwner, TerrainCell cell)
    {
        if(newOwner!= owner) 
        {
            OnCellChangeOwner?.Invoke(cell);
            RemoveCell(cell);
        }
    }
    private void ChangeUnitNumber(int previousNumber, int newNumber, TerrainCell cell)
    {
        unitNumber += newNumber - previousNumber;
    }

    private void ChangeNestCondition(bool previousCondition, bool newCondition, TerrainCell cell)
    {
        isNestBuilt = newCondition;
    }

    private void ChangeFoodNumber(int previousNumber, int newNumber, TerrainCell cell)
    {
        foodNumber += newNumber - previousNumber;
    }
}
