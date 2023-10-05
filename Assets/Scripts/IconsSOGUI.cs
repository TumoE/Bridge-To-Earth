#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IconsSO))]
public class IconsSOGUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (IconsSO)target;

        if (GUILayout.Button("Get Icons", GUILayout.Height(30)))
        {
            script.GetData();
        }

    }
}

#endif

