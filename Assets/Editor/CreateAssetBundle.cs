using UnityEditor;
using UnityEngine;
using System.Collections;

public class CreateAssetBundle : Editor
{
    [MenuItem("Assets/Create Asset Bundle")]
    static void ExportBundle()
    {
        string bundlePath = "Assets/AssetBundle/AssetBundleTest.unity3d";
        Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
        BuildPipeline.BuildAssetBundle(Selection.activeObject, selectedAssets, bundlePath, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneOSX);

    }
}
