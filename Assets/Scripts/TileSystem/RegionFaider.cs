using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TileSystem 
{ 
    public sealed class RegionFaider : MonoBehaviour
    {
        private InputManager _inputManager;
        private TerrainTilemap _terrainTilemap;
        private Vector3 _cursorPosition;

        private void Start()
        {
            _inputManager = InputManager.instance; 
            _terrainTilemap = GetComponent<TerrainTilemap>();
        }

        public void Update()
        {
            _cursorPosition = _inputManager.cursorPosition;
            if(_terrainTilemap.ContainTile(_cursorPosition)) 
            {
                Region region = _terrainTilemap.GetTile(_cursorPosition).region;
                if (region.isFade) 
                { 
                
                }
            }
        }
    }
}


