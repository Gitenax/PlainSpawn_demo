using System;
using UnityEngine;

namespace Data
{
    public class ObjectNames : MonoBehaviour
    {
        #pragma warning disable CS0649
        [SerializeField] private string[] _names;
                         private string   _jsonName = "ObjNames";
        #pragma warning restore CS0649

        
        public event Action<ObjectNames> NamesLoaded;


        public string[] Names => _names;

        private void Start()
        {
            var json = Resources.Load<TextAsset>(_jsonName);
            var jsonNames = JsonUtility.FromJson<JsonNames>(json.text);
            
            _names = jsonNames.names;
            NamesLoaded?.Invoke(this);
        }
        
        
        [Serializable]
        internal sealed class JsonNames
        {
            #pragma warning disable CS0649
            public string[] names;
            #pragma warning restore CS0649
        }
    }
}
