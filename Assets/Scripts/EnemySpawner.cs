using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private Transform _target;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private Portal _portal;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
        createFunc: () => CreateEnemy(),
        actionOnGet: (enemy) => SetStartParameters(enemy),
        actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
        actionOnDestroy: (enemy) => DestroyObjectInPool(enemy),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void OnEnable()
    {
        _portal.EnemyTriggered += OnEnemyTriggered;
    }

    private void OnDisable()
    {
        _portal.EnemyTriggered -= OnEnemyTriggered;
    }

    private void OnEnemyTriggered(Enemy enemy)
    {
        _pool.Release(enemy);
    }

    public void SpawnEnemy()
    {
        _pool.Get();
    }

    private Enemy CreateEnemy()
    {
        Enemy createdEnemy = Instantiate(_prefab);
        createdEnemy.SetTarget(_target);
        createdEnemy.SetType(_enemyType);
        createdEnemy.gameObject.SetActive(false);
        return createdEnemy;
    }

    private void SetStartParameters(Enemy enemy)
    {
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
    }

    private void DestroyObjectInPool(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}