using BattleSystem;
using UnityEngine;
using TileSystem;

public class Nest
{
    private Timer _timer = new();
    private TerrainCell _cell;
    private Unit _unit;
    private const float DEFAULT_TIME_TO_SPAWN_IN_NEST = 1;
    private const float DEFAULT_TIME_TO_SPAWN_WITHOUT_NEST = 3;
    public bool isNestBuild;
    public Nest(TerrainCell cell, bool isNestBuild)
    {
        _cell = cell;
        _unit = PlayersUnits.instance.GetUnit(_cell.owner);
        this.isNestBuild = isNestBuild;
        StartTimerToSpawnUnit();
    }
    ~Nest()
    {
        _timer.StopTimer();
    }

    private void StartTimerToSpawnUnit()
    {
        if (isNestBuild) 
        {
            _timer.StartTimer(DEFAULT_TIME_TO_SPAWN_IN_NEST * _unit.spawnSpeed);
            _timer.OnTimeOver -= SpawnUnit;
            _timer.OnTimeOver += SpawnUnit;
        }
        else 
        {
            _timer.StartTimer(DEFAULT_TIME_TO_SPAWN_WITHOUT_NEST * _unit.spawnSpeed);
            _timer.OnTimeOver -= SpawnUnit;
            _timer.OnTimeOver += SpawnUnit;
        }
    }

    private void SpawnUnit()
    {
        _cell.unitNumber++;
        StartTimerToSpawnUnit();
    }
}
