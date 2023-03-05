using UnityEngine;

public interface IGameState
{
    public void Entry(GameObject inputComponentParent);
    public void Exit(GameObject inputComponentParent);
}
