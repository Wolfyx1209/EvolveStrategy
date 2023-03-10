using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    private TextMeshProUGUI _unitsNumberTxt =>
        GetComponentInChildren<TextMeshProUGUI>();
    private Image _nestIcon =>
        GetComponentInChildren<Image>();

    private ViewFaider faider;
    [SerializeField] private float _fadeInDuration = 0.5f;
    [SerializeField] private float _fadeOutDuration = 0.5f;

    private void Awake()
    {
        faider = new();
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
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Image image))
            {
                faider.FadeOut(image, _fadeOutDuration);
            }
            if (transform.GetChild(i).TryGetComponent(out TextMeshProUGUI text))
            {
                faider.FadeOut(text, _fadeOutDuration);
            }
        }
    }

    public void ShowView(bool isBuilded)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Image image))
            {
                faider.FadeIn(image, _fadeOutDuration);
            }
            if (transform.GetChild(i).TryGetComponent(out TextMeshProUGUI text))
            {
                faider.FadeIn(text, _fadeOutDuration);
            }
        }
    }
}
