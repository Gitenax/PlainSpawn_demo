using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ColorSet", menuName = "Game Data/Color set", order = 50)]
    public class ColorSet : ScriptableObject
    {
        #pragma warning disable CS0649
        [SerializeField] private Color[] _availableColors;
        #pragma warning restore CS0649

        public Color[] Colors => _availableColors;
    }
}
