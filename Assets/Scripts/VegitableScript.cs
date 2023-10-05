using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegitableScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _vegitables;
    [SerializeField] private Collider _collider;

    [SerializeField] private float _calorie;
    [SerializeField] private float _growTime;

    private float _growTimer;

    private int _pointer;
    private int _respawnIndex = 0;

    private bool _startGrow;

    public bool Pull()
    {
        if (_pointer <= _vegitables.Length)
        {
            _vegitables[_pointer++].SetActive(false);

            if(_pointer >= _vegitables.Length)
            {
                if (_collider) _collider.enabled = false;
                _pointer = 0;
                _startGrow = true;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        Grow();
    }

    
    private void Grow()
    {
        if (!_startGrow) return;
       
        _growTimer += Time.deltaTime;

        if(_growTimer >= _growTime)
        {
            if (_respawnIndex < _vegitables.Length) _vegitables[_respawnIndex++].SetActive(true);
            _growTimer = 0;
        }

        if(_respawnIndex == _vegitables.Length)
        {
            _growTimer = 0;
            _respawnIndex = 0;
            if (_collider) _collider.enabled = true;
            _startGrow = false;
        }
    }

    public float Calorie
    {
        get { return _calorie; }
    }
}
