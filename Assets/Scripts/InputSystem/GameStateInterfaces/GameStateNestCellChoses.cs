using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateNestCellChoses : IGameState
{
    public void Entry(GameObject inputComponentParent)
    {
        inputComponentParent.GetComponent<ClickDetection>().enabled = true;
    }

    public void Exit(GameObject inputComponentParent)
    {
        inputComponentParent.GetComponent<ClickDetection>().enabled = false;
    }
}
