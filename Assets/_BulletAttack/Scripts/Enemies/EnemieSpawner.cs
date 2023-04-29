using System.Collections.Generic;
using UnityEngine;

public class EnemieSpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private Transform _attackDirection;
    [SerializeField] private Health _wall;
    [SerializeField] private Weapon _weapon;

    [EditorButton]
    private void ActivateEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.SetTarget(_wall, _attackDirection.forward);

            var enemiesHealth = new List<Health>();

            foreach (var en in _enemies)
            {
                enemiesHealth.Add(en.Health);
            }

            _weapon.SetEnemies(enemiesHealth);
        }
    }
}