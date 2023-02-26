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
        switch (owner)
        {
            case (PlayersList.Player):
                _unitsNumberTxt.faceColor = Color.yellow;
                break;
            case (PlayersList.None):
                _unitsNumberTxt.faceColor = Color.gray;
                break;
            case (PlayersList.Red):
                _unitsNumberTxt.faceColor = Color.red;
                break;
            case (PlayersList.Blue):
                _unitsNumberTxt.faceColor = Color.blue;
                break;
            case (PlayersList.Green):
                _unitsNumberTxt.faceColor = Color.green;
                break;
        }
    }

    public void UpdateNestView(bool isBuilded) =>
        _nestIcon.gameObject.SetActive(isBuilded);

    public void UpdateFoodView()
    {

    }
}
