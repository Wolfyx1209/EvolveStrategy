using EventBusSystem;
using TMPro;
using UnityEngine;

public class PlayerPoints : MonoBehaviour, IEvolvePointsChangeHandler
{
    public TextMeshProUGUI textField;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
        textField.text = Bank.instance.evolvePoints.ToString();
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
    public void EvolvePointsChanges(int value)
    {
        textField.text = value.ToString();
    }
}
