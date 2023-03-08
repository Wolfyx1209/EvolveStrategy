using BattleSystem;
using UnityEngine;
using TileSystem;

public class Nest : SimpleSpawner
{
    public Nest(TerrainCell cell) : base(cell)
    {
        DEFAULT_TIME_TO_SPAWN = 1;
    }
}
