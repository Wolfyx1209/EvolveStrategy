using BattleSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderDrawer : MonoBehaviour
{
    [SerializeField] private GameObject _aroow;
    [SerializeField] private Transform _parent;

    private Dictionary<IAttackComand, Image> arrows = new();

    public void NewComand(Vector3 fromCellPosition, Vector3 toCellPosition, IAttackComand comand)
    {
        Vector3 position = Vector3.Lerp(fromCellPosition, toCellPosition, 0.5f);
        Quaternion rotation = GetRorarionBetween(fromCellPosition, toCellPosition);
        GameObject newArrow = Instantiate(_aroow, position, rotation, _parent);
        Image image = newArrow.GetComponent<Image>();
        arrows.Add(comand, image);
    }

    private void Update()
    {
        List<IAttackComand> deleteList = new();
        foreach (KeyValuePair<IAttackComand, Image> pair in arrows)
        {
            if (pair.Key.GetProgress() >= 1)
            {
                deleteList.Add(pair.Key);
                continue;
            }
            pair.Value.fillAmount = pair.Key.GetProgress();
        }
        DeleteComands(deleteList);
    }

    private void DeleteComands(List<IAttackComand> comands)
    {
        foreach (IAttackComand comand in comands)
        {
            arrows.TryGetValue(comand, out Image image);
            Destroy(image.gameObject);
            arrows.Remove(comand);
        }
    }

    private Quaternion GetRorarionBetween(Vector3 from, Vector3 to)
    {
        Vector3 dir = (to - from).normalized;
        float angle = Vector2.SignedAngle(Vector3.up, new Vector3(dir.x, dir.y));
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
