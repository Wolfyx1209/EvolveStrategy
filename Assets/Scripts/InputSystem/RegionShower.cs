using TileSystem;
using UnityEngine;

public class RegionShower : MonoBehaviour
{
    private TerrainTilemap _terrainTilemap;
    private InputManager _inputManager;
    private GameStateManager _gameStateManager;

    private Region _previousRegion = null;

    private void Awake()
    {
        _inputManager = InputManager.instance;
        _terrainTilemap = FindObjectOfType<TerrainTilemap>();
        _gameStateManager = GameStateManager.instance;
    }

    private void Update()
    {
        if (_gameStateManager.currentState == GameStates.Battle)
        {
            if (_terrainTilemap.ContainTile(_inputManager.cursorPosition))
            {
                Region newRegion = _terrainTilemap.GetTile(_inputManager.cursorPosition).region;
                if (newRegion.isFade && newRegion != _previousRegion)
                {
                    newRegion.ShowCellsInfo();
                    if (_previousRegion != null)
                        _previousRegion.HideCellsInfo();
                    _previousRegion = newRegion;
                }
            }
            else
            {
                if (_previousRegion != null)
                {
                    _previousRegion.HideCellsInfo();
                    _previousRegion = null;
                }
            }
        }

    }
}
