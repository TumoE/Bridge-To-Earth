#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelObjectsRandomizerGUI : EditorWindow
{
    LevelObjectsRandomizerSO _objects;

    [MenuItem("MyTools/RandomizeLevel")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelObjectsRandomizerGUI));
    }

    private void OnGUI()
    {
        _objects = EditorGUILayout.ObjectField("Objects", _objects, typeof(LevelObjectsRandomizerSO), true) as LevelObjectsRandomizerSO;

        if (GUILayout.Button("Rotate", GUILayout.Height(20)))
        {
            _objects.RandomRotation();
        }
        if (GUILayout.Button("Scale", GUILayout.Height(20)))
        {
            _objects.RandomScale();
        }
    }
}
#endif
