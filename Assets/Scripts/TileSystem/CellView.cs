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

    public void UpdateUnitView(int newUnitNumber, PlayersList owner)
    {
        _unitsNumberTxt.text = newUnitNumber.ToString();
        float al = _unitsNumberTxt.faceColor.a;
        Color col = new PlayersColors().GetColor(owner);
        col.a = al;
        _unitsNumberTxt.faceColor = col;
    }

    public void UpdateNestView(bool isBuilded) 
    {
        Color col = _nestIcon.color;
        col.a = 0;
        _nestIcon.color = col;
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

    public void ShowView()
    {
        Color col = _unitsNumberTxt.faceColor;
        col.a = 1;
        _unitsNumberTxt.faceColor = col;
        col = _nestIcon.color;
        col.a = 1;
        _nestIcon.color = col;
    }
}
