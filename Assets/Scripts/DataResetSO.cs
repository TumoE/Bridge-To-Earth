using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Reset Data SO")]
public class DataResetSO : ScriptableObject
{
    [SerializeField] private List<ContentData> _allContentData;

    public void ResetAllContentData()
    {
        for (int i = 0; i < _allContentData.Count; i++)
        {
            _allContentData[i].Backup();
        }
    }
}
