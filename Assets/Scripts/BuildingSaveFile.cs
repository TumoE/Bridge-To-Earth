using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingSaveFile
{
    public bool isReady;
    public int[] value;

    public BuildingSaveFile(ContentData state)
    {
        value = new int[state.requirements.Count];
        isReady = state.isReady;

        for (int i = 0; i < state.requirements.Count; i++)
        {
            value[i] = state.requirements[i].value;
        }
    }
}
