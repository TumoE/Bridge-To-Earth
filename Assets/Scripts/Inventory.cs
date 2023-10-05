using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    private List<string> _tools = new List<string>();
    private List<int> _resourcesKeys = new List<int>();
    private Dictionary<int, int> _resources = new Dictionary<int, int>();

    public void AddResource(int resourceID, int count)
    {
        _resourcesKeys.Add(resourceID);

        _resources.Add(resourceID, count);
    }
    public void ChangeResourceCount(int resourceID,int value)
    {
        if (_resources.ContainsKey(resourceID))
        {
            _resources[resourceID] += value;
        }
    }
    
    public bool ContainsResource(int resourceID)
    {
        return _resources.ContainsKey(resourceID);
    }
    public int GetResourceCount(int resourceID)
    {
        if (_resources.ContainsKey(resourceID))
        {
            return _resources[resourceID];
        }

        return 0;
    }
    public int GetRecourceKey(int index)
    {
        return _resourcesKeys[index];
    }
    
    public void AddTool(string toolName)
    {
        if (!_tools.Contains(toolName))
        {
            _tools.Add(toolName);
        }
    }
    public string GetToolName(int index)
    {
        return _tools[index];
    }
    public int GetToolIndex(string toolName)
    {
        return _tools.FindIndex(a => a.Contains(toolName));
    }
    public int GetToolCount()
    {
        return _tools.Count;
    }

    public List<string> GetTools()
    {
        return _tools;
    }
    
    public List<int> ResourcesKeys
    {
        get { return _resourcesKeys; }
        set { _resourcesKeys = value; }
    }
    public Dictionary<int,int> Resources
    {
        get { return _resources; }
        set { _resources = value; }
    }


}
