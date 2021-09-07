using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScriptLoader : MonoBehaviour
{
    public TextAsset source;

    void ReadFile()
    {
        var text = source.text;
        print(text.Split('\n')[0]);
    }
}
