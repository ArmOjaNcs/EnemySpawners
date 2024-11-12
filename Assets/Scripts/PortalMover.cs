using UnityEngine;

public class PortalMover : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;

    private int _counter;

    private void Awake()
    {
        _counter = 0;
    }

    private void FixedUpdate()
    {
        if(transform.position == _points[_counter].position)
            _counter = (_counter + 1) % _points.Length;
        
        transform.position = Vector3.MoveTowards(transform.position, _points[_counter].position, _speed);
    }
}
