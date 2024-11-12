using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private EnemyType _enemyType;

    public event Action<Enemy> EnemyTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Enemy enemy))
        {
            if(enemy.Type == _enemyType)
                EnemyTriggered?.Invoke(enemy);
        }
    }
}
