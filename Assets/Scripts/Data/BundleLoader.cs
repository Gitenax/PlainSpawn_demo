using System;
using System.IO;
using UnityEngine;

namespace Data
{
    public class BundleLoader : MonoBehaviour
    {
        #pragma warning disable CS0649
                         private string       _bundleName = "primitives";
        [SerializeField] private GameObject[] _prefabs;
        #pragma warning restore CS0649
        
        
        public event Action<BundleLoader> BundleLoaded;
        
        
        public GameObject[] Prefabs => _prefabs;


        private void Start()
        {
            var bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, _bundleName));

            if (bundle == null)
            {
                Debug.LogWarning("Ошибка загрузки бандла");
                return;
            }

            _prefabs = bundle.LoadAllAssets<GameObject>();
            BundleLoaded?.Invoke(this);
            bundle.Unload(false);
        }
    }
}
