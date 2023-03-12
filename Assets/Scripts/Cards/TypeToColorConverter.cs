using UnityEngine;
namespace CardSystem
{
    [CreateAssetMenu(fileName = "CardPalette", menuName = "Cards/New CardPalette")]
    public class TypeToColorConverter : ScriptableObject
    {
        [Header("Mouth")]
        public Sprite MouthIcon;
        public Color MouthColor;

        [Header("InternalStructure")]
        public Sprite InternalStructureIcon;
        public Color InternalStructureColor;

        [Header("Head")]
        public Sprite HeadIcon;
        public Color HeadColor;

        [Header("Cover")]
        public Sprite CoverIcon;
        public Color CoverColor;

        [Header("Limbs")]
        public Sprite LimbsIcon;
        public Color LimbsColor;
    }
}

