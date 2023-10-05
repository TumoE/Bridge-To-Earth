using UnityEngine;

[CreateAssetMenu(fileName = "New Resource")]
[System.Serializable]
public class Resource : ScriptableObject
{
    public string resourceName;
    
    public int id;
    public bool isTool;

    public Sprite icon;
}
