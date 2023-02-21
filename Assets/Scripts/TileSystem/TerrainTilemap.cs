using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TileSystem
{
    public class TerrainTilemap : MonoBehaviour
    {
        private Dictionary<Vector2Int, TerrainTile> _terrainTilemap = new();
        private Tilemap _baseTilemap;
        private void Awake()
        {
            _baseTilemap = GetComponent<Tilemap>();
        }

        public void AddTerrainTile(Vector3 worldPosition, TerrainTile tileData) 
        {
            AddTerrainTile(
                GetGridPositionFromWorldCoordinate(worldPosition), tileData);
        }
        public void AddTerrainTile(Vector2Int gridPosition, TerrainTile tileData)
        {
            if (!_terrainTilemap.ContainsKey(gridPosition))
            {
                _terrainTilemap.Add(gridPosition, tileData);
            }
            else
            {
                throw new Exception("You already have tile in coordinate: " + gridPosition);
            }
        }

        public void RemoveTerrainTile(Vector3 worldPosition)
        {
            RemoveTerrainTile(
                GetGridPositionFromWorldCoordinate(worldPosition));
        }
        public void RemoveTerrainTile(Vector2Int gridPosition) => _terrainTilemap.Remove(gridPosition);

        public bool ContainTile(Vector3 worldCoordinate) 
        { 
            return ContainTile(
                GetGridPositionFromWorldCoordinate(worldCoordinate));
        }

        public bool ContainTile(Vector2Int gridPosition) => _terrainTilemap.ContainsKey(gridPosition);

        public TerrainTile GetTile(Vector3 worldPosition) 
        {
            return GetTile(
                GetGridPositionFromWorldCoordinate(worldPosition));
        }
        public TerrainTile GetTile(Vector2Int gridPosition) => _terrainTilemap[gridPosition];

        private Vector2Int GetGridPositionFromWorldCoordinate(Vector3 worldPosition) 
        {
            worldPosition.z = _baseTilemap.transform.position.z;
            Vector3Int gridPosition = _baseTilemap.WorldToCell(worldPosition);
            return new Vector2Int(gridPosition.x, gridPosition.y);
        }

    }

}
