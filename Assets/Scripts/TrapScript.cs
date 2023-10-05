using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField] private Transform _spear;
    [SerializeField] private AnimationCurve _curve;

    [SerializeField] private float _speed;

    private float _timer;

    void Update()
    {
        SpearMove();
    }

    private void SpearMove()
    {
        _timer += Time.deltaTime * _speed;

        _spear.transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.up * 2, _curve.Evaluate(_timer));

    }
}
