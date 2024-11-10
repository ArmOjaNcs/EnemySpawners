using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private Transform _target;

    private ObjectPool<Enemy> _pool;
    private Vector3 _direction;

    private void Awake()
    {
        _direction = _target.transform.position;

        _pool = new ObjectPool<Enemy>(
        createFunc: () => CreateEnemy(),
        actionOnGet: (enemy) => SetStartParameters(enemy),
        actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
        actionOnDestroy: (enemy) => DestroyObjectInPool(enemy),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    public void GetEnemy()
    {
        _pool.Get();
    }

    private Enemy CreateEnemy()
    {
        Enemy createdEnemy = Instantiate(_prefab);
        createdEnemy.Finished += OnFinished;
        createdEnemy.SetDirection(_direction);
        createdEnemy.gameObject.SetActive(false);
        return createdEnemy;
    }

    private void SetStartParameters(Enemy enemy)
    {
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
    }

    private void OnFinished(Enemy enemy)
    {
        _pool.Release(enemy);
    }

    private void DestroyObjectInPool(Enemy enemy)
    {
        enemy.Finished -= OnFinished;
        Destroy(enemy.gameObject);
    }
}