using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.1f)] private float _speed;
    [SerializeField] private Vector3 _direction;

    private readonly float _acceleration = 0.001f;
    private readonly float _speedLimit = 0.25f;

    private float _currentSpeed;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Portal>(out Portal _))
            Finished?.Invoke(this);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void MoveToDirection()
    {
        transform.position = Vector3.MoveTowards(transform.position, _direction, _currentSpeed);
        transform.LookAt(_direction);
    }
}
