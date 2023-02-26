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
        public List<Vector2Int> GetCoordinatesOfNeighboringCells(TerrainCell cell)
        {
            Vector3 worldCoordinate = cell.transform.position;
            return GetCoordinatesOfNeighboringCells(WorldPositionToGrid(worldCoordinate));
        }
        public List<Vector2Int> GetCoordinatesOfNeighboringCells(Vector2Int coordinate)
        {
            List<Vector2Int> coordinates = new();
            coordinates.Add(new Vector2Int(coordinate.x - 1, coordinate.y));
            coordinates.Add(new Vector2Int(coordinate.x + 1, coordinate.y));
            coordinates.Add(new Vector2Int(coordinate.x, coordinate.y - 1));
            coordinates.Add(new Vector2Int(coordinate.x, coordinate.y + 1));
            if (coordinate.y % 2 == 0)
            {
                coordinates.Add(new Vector2Int(coordinate.x - 1, coordinate.y + 1));
                coordinates.Add(new Vector2Int(coordinate.x - 1, coordinate.y - 1));
            }
            else
            {
                coordinates.Add(new Vector2Int(coordinate.x + 1, coordinate.y + 1));
                coordinates.Add(new Vector2Int(coordinate.x + 1, coordinate.y - 1));
            }
            return coordinates;
        }

        public List<TerrainCell> GetCellNeighbors(TerrainCell terrainCell)
        {
            List<TerrainCell> neighbors = new();
            List<Vector2Int> neighdorsCoordinate =
                GetCoordinatesOfNeighboringCells(terrainCell);
            foreach (Vector2Int neighborCoordinate in neighdorsCoordinate)
            {
                if (_terrainTilemap.TryGetValue(neighborCoordinate, out TerrainCell cell))
                {
                    neighbors.Add(cell);
                }
            }
            return neighbors;
        }

        public bool isCellsNeighbours(TerrainCell a, TerrainCell b)
        {
            return GetCellNeighbors(a).Contains(b);
        }
        public Vector2Int WorldPositionToGrid(Vector3 worldPosition)
        {
            worldPosition.z = _baseTilemap.transform.position.z;
            Vector3Int gridPosition = _baseTilemap.WorldToCell(worldPosition);
            return new Vector2Int(gridPosition.x, gridPosition.y);
        }
        private void InitializeTerrainMap()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                AddToTilemapIfObjectIsCell(transform.GetChild(i));
            }

            foreach (TerrainCell cell in _terrainTilemap.Values)
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
                if (!_terrainTilemap.ContainsValue(currentCell))
                {
                    _terrainTilemap.Add(
                        WorldPositionToGrid(currentCell.transform.position),
                        currentCell);
                }
                else
                {
                    throw new Exception("You already have tile in coordinate: " +
                        WorldPositionToGrid(currentCell.transform.position));
                }
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
                List<TerrainCell> neighborhoodCells =
                    GetCellNeighbors(currentCell);
                foreach (TerrainCell neighbour in neighborhoodCells)
                {
                    if (neighbour.region == null && neighbour.cellType == startCell.cellType)
                    {
                        cellToAnalyz.Enqueue(neighbour);
                    }
                }
            }
        }
    }

}
