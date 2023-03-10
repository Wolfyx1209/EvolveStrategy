using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    private TextMeshProUGUI _unitsNumberTxt =>
        GetComponentInChildren<TextMeshProUGUI>();
    private Image _nestIcon =>
        GetComponentInChildren<Image>();

    private ViewFaider _faider;

    private void Awake()
    {
        _faider = new();
        transform.transform.localScale =
            gameObject.GetComponentInParent<Transform>().localScale;
    }

    public void UpdateUnitView(int newUnitNumber, PlayersList owner, bool isShowen)
    {
        _unitsNumberTxt.text = newUnitNumber.ToString();
        Color col = new PlayersColors().GetColor(owner);
        _unitsNumberTxt.faceColor = new Color(col.r, col.g, col.b, _unitsNumberTxt.faceColor.a);
    }

    public void UpdateNestView(bool isBuilt, bool isShowen)
    {
        _nestIcon.enabled = isBuilt;
        Color col = _nestIcon.color;
        col.a = isShowen ? 1 : 0;
        _nestIcon.color = col;
    }

    public void UpdateFoodView()
    {

    }

    public void HideView()
    {
        _faider.FadeOutAllView(transform);
    }

    public void ShowView(bool isBuilded)
    {
        _faider.FadeInAllView(transform);
    }
}
