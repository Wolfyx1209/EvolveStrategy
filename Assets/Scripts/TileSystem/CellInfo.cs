using UnityEngine;
namespace TileSystem 
{
    [CreateAssetMenu(fileName = "CellInfo", menuName = "Terrain/New CellInfo")]
    public class CellInfo : ScriptableObject
    {
        [SerializeField] private string _type;

        public string Type => _type;
    }
}
