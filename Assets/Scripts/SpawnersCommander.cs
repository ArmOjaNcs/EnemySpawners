using System.Collections;
using UnityEngine;

public class SpawnersCommander : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _enemySpawners;
    [SerializeField] private float _rate;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_rate);
    }

    private void Start()
    {
        StartCoroutine(SpawningEnemy());
    }

    private IEnumerator SpawningEnemy()
    {
        while (true)
        {
            int spawnerIndex = Random.Range(0, _enemySpawners.Length);
            _enemySpawners[spawnerIndex].GetEnemy();
            yield return _wait;
        }
    }
}
