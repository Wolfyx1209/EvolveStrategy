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

    public void ChangeUnitView(int newUnitNumber) => 
        _unitsNumberTxt.text = newUnitNumber.ToString();

    public void ChangeNestCondition(bool isBuilded) =>
        _nestIcon.gameObject.SetActive(isBuilded);
}
