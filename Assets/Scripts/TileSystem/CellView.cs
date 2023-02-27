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
        _unitsNumberTxt.faceColor = new PlayersColors().GetColor(owner);
    }

    public void UpdateNestView(bool isBuilded) =>
        _nestIcon.gameObject.SetActive(isBuilded);

    public void UpdateFoodView()
    {

    }
}
