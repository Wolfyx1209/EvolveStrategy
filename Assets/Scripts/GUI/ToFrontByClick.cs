using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToFrontByClick : MonoBehaviour, IPointerClickHandler
{
    public Transform tr;
    public void OnPointerClick(PointerEventData eventData)
    {
        tr.SetAsLastSibling();
    }
}
