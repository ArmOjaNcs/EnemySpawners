using UnityEngine;

public class PortalMover : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;

    private int _indexOfPoint;

    private void Awake()
    {
        _indexOfPoint = 0;
    }

    private void FixedUpdate()
    {
        if(transform.position == _points[ _indexOfPoint].position)
            _indexOfPoint = ++_indexOfPoint % _points.Length;
        
        transform.position = Vector3.MoveTowards(transform.position, _points[_indexOfPoint].position, _speed);
    }
}
