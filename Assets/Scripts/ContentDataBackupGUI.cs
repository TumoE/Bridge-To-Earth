
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ContentData))]
public class ContentDataBackupGUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (ContentData)target;

        if (GUILayout.Button("Backup", GUILayout.Height(40)))
        {
            script.Backup();
        }

    }
}
#endif
