using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public sealed class PlayersUnits : MonoBehaviour
    {
        private static Dictionary<PlayersList, Unit> units = new();
        private static PlayersUnits instance
        {
            get
            {
                if (m_instance == null)
                {
                    GameObject gameObject = new GameObject("Player Units Repository");
                    m_instance = gameObject.AddComponent<PlayersUnits>();
                }
                return m_instance;
            }
        }
        private static PlayersUnits m_instance;

        public static Unit GetUnit(PlayersList owner)
        {
            if (!units.ContainsKey(owner))
            {
                units.Add(PlayersList.Player, new Unit(PlayersList.Player));
            }
            return units[owner];
        }
    }
}

