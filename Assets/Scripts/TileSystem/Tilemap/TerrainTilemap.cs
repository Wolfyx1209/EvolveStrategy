using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TileSystem
{
    public class TerrainTilemap : MonoBehaviour
    {
        private Dictionary<Vector2Int, TerrainCell> _terrainTilemap = new();
        private Tilemap _baseTilemap;
        private void Awake()
        {
            _baseTilemap = GetComponent<Tilemap>();
            InitializeTerrainMap();
        }

        public void AddTerrainTile(Vector3 worldPosition, TerrainCell tileData) 
        {
            AddTerrainTile(
                WorldPositionToGrid(worldPosition), tileData);
        }
        public void AddTerrainTile(Vector2Int gridPosition, TerrainCell tileData)
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
                WorldPositionToGrid(worldPosition));
        }
        public void RemoveTerrainTile(Vector2Int gridPosition) => _terrainTilemap.Remove(gridPosition);

        public bool ContainTile(Vector3 worldCoordinate) 
        { 
            return ContainTile(
                WorldPositionToGrid(worldCoordinate));
        }

        public bool ContainTile(Vector2Int gridPosition) => _terrainTilemap.ContainsKey(gridPosition);

        public TerrainCell GetTile(Vector3 worldPosition) 
        {
            return GetTile(
                WorldPositionToGrid(worldPosition));
        }
        public TerrainCell GetTile(Vector2Int gridPosition) => _terrainTilemap[gridPosition];

        private void InitializeTerrainMap() 
        {
            for(int i = 0; i < transform.childCount; i++) 
            {
                AddToTilemapIfObjectIsCell(transform.GetChild(i));
            }

            foreach(TerrainCell cell in _terrainTilemap.Values) 
            {
                if (cell.region == null)
                {
                    CreateRegionForCell(cell);
                } 
            }
        }

        private void AddToTilemapIfObjectIsCell(Transform objectTransform) 
        {
            if (objectTransform.TryGetComponent(out TerrainCell currentCell))
            {
                _terrainTilemap.Add(
                    WorldPositionToGrid(currentCell.transform.position),
                    currentCell);
            }
        }

        private Region CreateRegionForCell(TerrainCell cell) 
        {
            Region newRegion = new();
            DefineAllCellsForNewRegion(cell, newRegion);
            newRegion.DrawRegionBoundes();
            return newRegion;
        }

        private void DefineAllCellsForNewRegion(TerrainCell startCell, Region region) 
        {
            Queue<TerrainCell> cellToAnalyz = new();
            cellToAnalyz.Enqueue(startCell);

            while (cellToAnalyz.TryDequeue(out TerrainCell currentCell))
            {
                region._regionCells.Add(currentCell);
                currentCell.region = region;
                Vector2Int[] neighborhoodCellsCoordinate =
                    GetCoordinatesOfNeighboringCells(currentCell);

                foreach (Vector2Int coordinate in neighborhoodCellsCoordinate)
                {
                    if (_terrainTilemap.ContainsKey(coordinate)) 
                    { 
                        TerrainCell cell = _terrainTilemap[coordinate];
                        if (cell.region == null && cell.cellType == startCell.cellType)
                        {
                            cellToAnalyz.Enqueue(cell);
                        }                    
                    }

                }
            }
        }
        private Vector2Int[] GetCoordinatesOfNeighboringCells(TerrainCell cell)
        {
            Vector3 worldCoordinate = cell.transform.position;
            return GetCoordinatesOfNeighboringCells(WorldPositionToGrid(worldCoordinate));
        }
        private Vector2Int[] GetCoordinatesOfNeighboringCells(Vector2Int coordinate) 
        {
            Vector2Int[] coordinates = new Vector2Int[6];
            coordinates[0] = new Vector2Int(coordinate.x - 1, coordinate.y);
            coordinates[1] = new Vector2Int(coordinate.x, coordinate.y + 1);
            coordinates[2] = new Vector2Int(coordinate.x + 1, coordinate.y + 1);
            coordinates[3] = new Vector2Int(coordinate.x + 1, coordinate.y);
            coordinates[4] = new Vector2Int(coordinate.x + 1, coordinate.y-1);
            coordinates[5] = new Vector2Int(coordinate.x, coordinate.y - 1);
            return coordinates;
        }

        private Vector2Int WorldPositionToGrid(Vector3 worldPosition) 
        {
            worldPosition.z = _baseTilemap.transform.position.z;
            Vector3Int gridPosition = _baseTilemap.WorldToCell(worldPosition);
            return new Vector2Int(gridPosition.x, gridPosition.y);
        }

    }

}
