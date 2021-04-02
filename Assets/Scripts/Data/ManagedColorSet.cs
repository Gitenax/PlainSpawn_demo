using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ManagedColorSet", menuName = "Game Data/Managed Color set", order = 51)]
    public class ManagedColorSet : ScriptableObject
    {
        #pragma warning disable CS0649
        [SerializeField] private ClickColorPair[] _clickPairs;
        #pragma warning restore CS0649
        
        public ClickColorPair[] Pairs => _clickPairs;


        [System.Serializable]
        public struct ClickColorPair
        {
            public int   Clicks;
            public Color Color;
        }
    }
}