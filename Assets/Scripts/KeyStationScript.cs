using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyStationScript : MonoBehaviour
{
    [SerializeField] private OffetScript _offer;

    private bool _isOpned;

    public void Open()
    {
        IsOpned = _offer.TakeResource();
        if (IsOpned)
        {
            _offer.Drop();
        }
    }

    public bool IsOpned
    {
        get { return _isOpned; }
        set 
        { 
            _isOpned = value;

            if (_isOpned)
            {
                Destroy(gameObject,0.05f);
            }
        }
    }
}

