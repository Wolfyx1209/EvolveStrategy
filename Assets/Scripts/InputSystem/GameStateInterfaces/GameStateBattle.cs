using UnityEngine;

public class GameStateBattle : IGameState
{
    public void Entry(GameObject inputComponentParent)
    {
        inputComponentParent.GetComponent<SwipeDetection>().enabled = true;
        inputComponentParent.GetComponent<RegionShower>().enabled = true;
    }

    public void Exit(GameObject inputComponentParent)
    {
        inputComponentParent.GetComponent<SwipeDetection>().enabled = false;
        inputComponentParent.GetComponent<RegionShower>().enabled = false;
    }
}
