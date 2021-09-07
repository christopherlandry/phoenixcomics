using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class BundleAssets
{
    static string bundleDir = "Assets/Bundler/output"; //location bundle will be dropped

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAssetBundles()
    {
        consumeInFolder(); //flags everything in inputDir to be bundled
        BuildPipeline.BuildAssetBundles(bundleDir, BuildAssetBundleOptions.ForceRebuildAssetBundle, BuildTarget.Android);
        //BuildPipeline.BuildAssetBundles(bundleDir, BuildAssetBundleOptions.None, BuildTarget.iOS);
    }

    static void consumeInFolder()
    {
        foreach(string folder in Directory.GetDirectories(Application.dataPath + "/Resources/"))
        {
            var inputDir = folder.Split('/').Last();
            Object[] textures = Resources.LoadAll(inputDir, typeof(Texture2D));
            foreach (var t in textures)
            {
                MonoBehaviour.print("adding texture " + t.name);
                addAssetToOutBundle(inputDir, t);
            }
            Object[] scripts = Resources.LoadAll(inputDir, typeof(TextAsset));
            foreach (var s in scripts)
            {
                MonoBehaviour.print("adding script " + s.name);
                addAssetToOutBundle(inputDir, s);
            }
        }
    }

    static void addAssetToOutBundle(string bundleName, Object o)
    {
        var assetPath = AssetDatabase.GetAssetPath(o); 
        AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(bundleName, "");
    }
}
