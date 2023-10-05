using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Transform _cameraPivot;

    private void Start()
    {
        _cameraPivot = Camera.main.transform;
        transform.LookAt(transform.position + _cameraPivot.forward);
    }
}
