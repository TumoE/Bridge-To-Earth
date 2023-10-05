using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Content Data")]

public class ContentData : ScriptableObject
{
    public List<Requirements> requirements;
    public bool isReady;

    [SerializeField] private List<Requirements> backup;

    public void Backup()
    {
        requirements = backup;
        isReady = false;
    }

}
