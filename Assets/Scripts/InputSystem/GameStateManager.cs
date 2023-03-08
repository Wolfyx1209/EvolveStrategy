using UnityEngine;
using EventBusSystem;
using TileSystem;

[RequireComponent(typeof(ClickDetection), typeof(SwipeDetection), typeof(RegionShower))] 
public class GameStateManager : MonoBehaviour, IPlayerChoosesNestCellHandler
{
    private IGameState currentState;

    public void StartState(Region region)
    {
        ChangeCurrentState(new GameStateNestCellChoses());
    }
    public void EndState(Region region) 
    {
        ChangeCurrentState(new GameStateBattle());
    }

    private void Awake()
    {
        currentState = new GameStateBattle();
        currentState.Entry(gameObject);
        EventBus.Subscribe(this);
    }

    private void ChangeCurrentState(IGameState newState)
    {
        if(currentState != newState) 
        {
            currentState.Exit(gameObject);
            currentState = newState;
            currentState.Entry(gameObject);
        }
    }
}
