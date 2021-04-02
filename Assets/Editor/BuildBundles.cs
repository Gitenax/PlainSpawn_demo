using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class BuildBundles
    {
        [MenuItem("Assets/Create asset bundles")]
        private static void BuildAllBundles()
        {
            BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }
    }
}
