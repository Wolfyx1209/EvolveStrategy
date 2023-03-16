using EventBusSystem;
using TMPro;
using UnityEngine;

public class PlayerPoints : MonoBehaviour, IEvolvePointsChangeHandler
{
    public TextMeshProUGUI textField;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
        textField.text = Bank.instance.GetAcktorPoints(PlayersList.Player).ToString();
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
    public void EvolvePointsChanges(PlayersList acktor, int value)
    {
        if(acktor == PlayersList.Player) 
        {
            textField.text = value.ToString();
        }
    }
}
