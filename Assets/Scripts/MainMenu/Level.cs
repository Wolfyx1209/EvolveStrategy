using UnityEngine;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _levelNumber;
    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<LevelLoader>().LoadLevel(_levelNumber);
    }
}
