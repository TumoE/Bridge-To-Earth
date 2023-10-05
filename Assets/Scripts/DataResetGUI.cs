
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DataResetSO))]
public class DataResetGUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (DataResetSO)target;

        if (GUILayout.Button("Reset", GUILayout.Height(40)))
        {
            script.ResetAllContentData();
        }

    }
}
#endif
