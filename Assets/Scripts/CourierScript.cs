using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourierScript : MonoBehaviour
{
    [SerializeField] private OffetScript _offer;

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Collider _collider;

    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;

    [SerializeField] private float _travelTimer;

    private float _travelTime;
    private float _timeStep;

    private bool _isOrdered;

    private void Start()
    {

    }

    public void Init()
    {
        SetTimer();
    }
    public void GetData(out bool isOrdered, out float timer)
    {
        isOrdered = _isOrdered;
        timer = _travelTime;
    }
    public void LoadData(bool isOrdered,float timer)
    {
        _isOrdered = isOrdered;
        _travelTime = timer;
        _collider.enabled = !_isOrdered;
    }

    private void Update()
    {
        Move();
    }

    public void Order()
    {
        _isOrdered = _offer.TakeResource();
        _collider.enabled = !_isOrdered;
    }
    private void Move()
    {
        if (!_isOrdered) return;

        _travelTime -= Time.deltaTime;
        _timeStep = _travelTime/_travelTimer;

        transform.position = Vector3.Lerp(_startPosition, _endPosition, _curve.Evaluate(_timeStep));

        if (_travelTime <= 0)
        {
            EndOfRide();
        }

    }

    void EndOfRide()
    {
        SetTimer();
        _isOrdered = false;
        _collider.enabled = !_isOrdered;

        Values.CourierOfferId = Random.Range(0, _offer.OffersCount);
        _offer.Drop();
        _offer.LoadOffer(Values.CourierOfferId);
    }

    private void SetTimer()
    {
        _travelTime = _travelTimer;
    }
}
