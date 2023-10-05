using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Icons")]
public class IconsSO: ScriptableObject
{
    [SerializeField] private Resource[] resources;

    [SerializeField] private List<int> id;
    [SerializeField] private List<Sprite> icon;

    public Sprite GetIcon(int resourceId)
    {
        int index = id.FindIndex(a => a == resourceId);
        return icon[index];
    }

    public void GetData()
    {
        id.Clear();
        icon.Clear();

        for (int i = 0; i < resources.Length; i++)
        {
            id.Add(resources[i].id);
            icon.Add(resources[i].icon);
        }
        
    }

    private void Awake()
    {
        GetData();
    }

}
