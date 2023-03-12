using TMPro;
using UnityEngine;

public class TextColorChanger : MonoBehaviour, IMainColorChanger
{
    public void ChangeColorTo(Color color)
    {
        GetComponent<TextMeshProUGUI>().faceColor = color;
    }
}
