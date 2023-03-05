using TileSystem;

public class Nest
{
    private Timer _timer = new();
    private TerrainCell _cell;
    private float _timeToSpawn = 1;
    public Nest(TerrainCell cell)
    {
        _cell = cell;
        StartTimerToSpawnUnit();
    }
    ~Nest()
    {
        _timer.StopTimer();
    }

    private void StartTimerToSpawnUnit()
    {
        _timer.StartTimer(_timeToSpawn);
        _timer.OnTimeOver += SpawnUnit;
    }

    private void SpawnUnit()
    {
        _cell.unitNumber++;
        _timer.StartTimer(_timeToSpawn);
    }
}
