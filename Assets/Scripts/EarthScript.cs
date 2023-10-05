using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthScript : MonoBehaviour
{
    [SerializeField] private Transform _planet;
    [SerializeField] private float _speed;

    private void Start()
    {
        
    }
    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        _planet.transform.Rotate(-transform.forward * _speed * Time.deltaTime);
    }
}
