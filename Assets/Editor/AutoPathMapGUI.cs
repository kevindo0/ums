using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AutoPathMap))]
public class AutoPathGUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AutoPathMap autoPathMap = target as AutoPathMap;
        autoPathMap = target as AutoPathMap;
        if (GUILayout.Button("Rescan"))
        {
            autoPathMap.Scan();
        }
    }
}
