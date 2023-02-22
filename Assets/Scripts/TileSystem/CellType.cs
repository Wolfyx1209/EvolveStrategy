using UnityEngine;
namespace TileSystem 
{
    [CreateAssetMenu(fileName = "CellInfo", menuName = "Terrain/New CellInfo")]
    public class CellType : ScriptableObject
    {
        [SerializeField] private string _type;

        public string Type => _type;
    }
}
