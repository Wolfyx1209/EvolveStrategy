using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public sealed class PlayersUnits : Singletone<PlayersUnits>
    {
        private Dictionary<PlayersList, Unit> units = new();

        public Unit GetUnit(PlayersList owner)
        {
            if (!units.ContainsKey(owner))
            {
                units.Add(owner, new Unit(owner));
            }
            return units[owner];
        }
    }
}

