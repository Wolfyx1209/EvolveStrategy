using UnityEngine;

public class PlayersColors
{
    public Color GetColor(PlayersList player) 
    {
        switch (player) 
        { 
            case PlayersList.None:
                return Color.gray;

            case PlayersList.Player:
                return Color.yellow;

            case PlayersList.Red:
                return Color.red;

            case PlayersList.Blue:
                return Color.blue;

            case PlayersList.Green:
                return Color.green;

            default: 
                return Color.white;
        }
    }
}
