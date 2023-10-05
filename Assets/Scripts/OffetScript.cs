using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffetScript : MonoBehaviour
{
    [SerializeField] private List<ContentData> _offers;
    [SerializeField] private PickableResourceScript[] _newResources;
    [SerializeField] private RequirementsCanvasScript _requirementsCanvas;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _spawnRotation;

    private PlayerInventory _playerInventory;
    private ContentData offer;

    private int _pointer;
    private int _dealCout;
    private int _offerPointer;

    private bool _waitUntilDrop;

    void Start()
    {
        ScriptInit();
        LoadOffer(0);
    }

    public void LoadOffer(int id)
    {
        if (transform.CompareTag(Values.CourierTag)) 
        {
            _requirementsCanvas.Clear();
            id = Values.CourierOfferId;
        }

        _offerPointer = id;
        offer = _offers[_offerPointer];

        _requirementsCanvas.SetCanvasInfo(offer.requirements);
        _requirementsCanvas.SetCanvasInfo(_newResources);
    }
    public bool TakeResource()
    {
        if (_waitUntilDrop) return false;

        for (int i = 0; i < offer.requirements.Count; i++)
        {
            Resource neededResource = offer.requirements[i].resource;
            int neededCount = offer.requirements[i].value;

            if (_playerInventory.HasResource(neededResource.id) && _playerInventory.HasCount(neededResource.id) >= neededCount)
            {
                _dealCout++;
            }
        }

        if(_dealCout == offer.requirements.Count)
        {
            _waitUntilDrop = true;
            TakeFromInventory();
            return true;
        }

        _dealCout = 0;
        return false;
    }
    
    public void Drop()
    {
        for (int i = 0; i < _newResources.Length; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x + _offset.x , transform.position.y + _offset.y, transform.position.z + _offset.z);
            Instantiate(_newResources[i], spawnPosition, Quaternion.Euler(_spawnRotation));
        }

        _waitUntilDrop = false;
        _dealCout = 0;
    }

    private void TakeFromInventory()
    {
        for (int i = 0; i < offer.requirements.Count; i++)
        {
            Resource neededResource = offer.requirements[i].resource;
            int neededCount = offer.requirements[i].value;

            _playerInventory.UseFromInventory(neededResource.id, neededCount);
        }
    }
    
    private void ScriptInit()
    {
        _playerInventory = PlayerInventory.Script;
    }
    public int OffersCount
    {
        get { return _offers.Count; }
    }

}
