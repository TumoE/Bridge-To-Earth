using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothness;

    private void LateUpdate()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        Vector3 target = new Vector3(_target.transform.position.x + _offset.x, transform.position.y, _target.transform.position.z + _offset.z);
        transform.position = Vector3.Slerp(transform.position, target, _smoothness);
    }
}
