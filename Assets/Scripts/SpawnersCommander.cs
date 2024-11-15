using System.Collections;
using UnityEngine;

public class SpawnersCommander : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _enemySpawners;
    [SerializeField] private float _rate = 2f;

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
        while (enabled)
        {
            int spawnerIndex = Random.Range(0, _enemySpawners.Length);
            _enemySpawners[spawnerIndex].SpawnEnemy();
            yield return _wait;
        }
    }
}
