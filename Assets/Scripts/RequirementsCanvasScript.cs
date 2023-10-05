using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementsCanvasScript : MonoBehaviour
{
    [SerializeField] private ItemUI _ItemUI;
    [SerializeField] private GameObject _giveIcon;
    [SerializeField] private Transform _infoPanel;

    private List<ItemUI> _requarmentsInfo = new List<ItemUI>();

    public void SetCanvasInfo(ContentData state)
    {
        for (int i = 0; i < state.requirements.Count; i++)
        {
            ItemUI item = Instantiate(_ItemUI, _infoPanel);
            item.SetIcon(state.requirements[i].resource.icon);
            item.SetCountText(state.requirements[i].value);
            _requarmentsInfo.Add(item);
        }
    }
    public void SetCanvasInfo(List<Requirements> requirements)
    {
        for (int i = 0; i < requirements.Count; i++)
        {
            ItemUI item = Instantiate(_ItemUI, _infoPanel);
            item.SetIcon(requirements[i].resource.icon);
            item.SetCountText(requirements[i].value);
            _requarmentsInfo.Add(item);
        }
    }
    public void SetCanvasInfo(PickableResourceScript[] newResources)
    {
        GameObject giveIcon = Instantiate(_giveIcon, _infoPanel);
        ItemUI item = Instantiate(_ItemUI, _infoPanel);
        item.SetIcon(newResources[0].GetResource.icon);
        item.SetCountText(newResources.Length);
        _requarmentsInfo.Add(item);
    }

    public void RemoveRequirementInfo(int pointer)
    {
        Destroy(_requarmentsInfo[pointer].gameObject);
        _requarmentsInfo.RemoveAt(pointer);
    }

    public void ChangeCanvasInfo(List<Requirements> _requarments)
    {
        for (int i = 0; i < _requarmentsInfo.Count; i++)
        {
            _requarmentsInfo[i].SetCountText(_requarments[i].value);
        }
    }
    public void Clear()
    {
        for (int i = 0; i < _infoPanel.childCount; i++)
        {
            Destroy(_infoPanel.GetChild(i).gameObject);
        }
    }
}
