using UnityEngine;
using UnityEngine.UI;

public class ArrowView : MonoBehaviour
{
    private Image filler;
    private void OnEnable()
    {
        filler = transform.GetChild(0).GetComponent<Image>();
    }
    public void Refill(float progress)
    {
        filler.fillAmount = progress;
    }
    public void InstanceColor(Color color)
    {
        color.a = 1f;
        filler.color = color;

        color.a = 0.5f;
        Image substrate = GetComponent<Image>();
        substrate.color = color;
    }
}
