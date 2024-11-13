using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.1f)] private float _speed;
    [SerializeField, Range(0.0005f, 0.001f)] private float _acceleration;
    [SerializeField, Range(0.1f, 0.25f)] private float _speedLimit;
    
    private readonly float _minDistanceToFinish = 2;
    
    private Transform _target;
    private float _currentSpeed;
    Vector3 _distance;

    public event Action<Enemy> Finished;

    private void OnEnable()
    {
        _currentSpeed = _speed;
    }

    private void FixedUpdate()
    {
        if (_currentSpeed < _speedLimit)
            _currentSpeed += _acceleration;

        MoveToDirection();
    }

    private void Update()
    {
        _distance = transform.position - _target.position;

        if (_distance.magnitude < _minDistanceToFinish)
            Finished?.Invoke(this);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void MoveToDirection()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _currentSpeed);
        transform.LookAt(_target);
    }
}
