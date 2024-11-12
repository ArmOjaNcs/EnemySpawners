using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.1f)] private float _speed;
    [SerializeField, Range(0.0005f, 0.001f)] private float _acceleration;
    [SerializeField, Range(0.1f, 0.25f)] private float _speedLimit;
    
    private Transform _target;
    private float _currentSpeed;
    private EnemyType _type;

    public EnemyType Type => _type;

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

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetType(EnemyType type)
    {
        _type = type;
    }

    private void MoveToDirection()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _currentSpeed);
        transform.LookAt(_target);
    }
}
