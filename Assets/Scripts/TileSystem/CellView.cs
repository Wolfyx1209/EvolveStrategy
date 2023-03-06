using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    private TextMeshProUGUI _unitsNumberTxt =>
        GetComponentInChildren<TextMeshProUGUI>();
    private Image _nestIcon =>
        GetComponentInChildren<Image>();

    private void Awake()
    {
        transform.transform.localScale =
            gameObject.GetComponentInParent<Transform>().localScale;
    }

    public void UpdateUnitView(int newUnitNumber, PlayersList owner, bool isShowen)
    {
        _unitsNumberTxt.text = newUnitNumber.ToString();
        Color col = new PlayersColors().GetColor(owner);
        col.a = isShowen ? 1 : 0;
        _unitsNumberTxt.faceColor = col;
    }

    public void UpdateNestView(bool isBuilded, bool isShowen) 
    {
        if (isShowen) 
        {
            Color col = _nestIcon.color;
            col.a = isBuilded ? 1 : 0;
            _nestIcon.color = col;
        }
    }

    public void UpdateFoodView()
    {

    }

    public void HideView() 
    {
        Color col = _unitsNumberTxt.faceColor;
        col.a = 0;
        _unitsNumberTxt.faceColor = col;
        col = _nestIcon.color;
        col.a = 0;
        _nestIcon.color = col;
    }

    public void ShowView(bool isBuilded)
    {
        Color col = _unitsNumberTxt.faceColor;
        col.a = 1;
        _unitsNumberTxt.faceColor = col;
        UpdateNestView(isBuilded, true);
    }
}
