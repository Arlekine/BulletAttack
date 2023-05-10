using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public record EnemyWave 
{
    [Serializable]
    private class EnemyVariant
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private int _amount;

        public Enemy Enemy => _enemy;
        public int Amount => _amount;
    }

    [SerializeField] private float _timeBeforeWaveInSeconds;
    [SerializeField] private EnemyVariant[] _enemyVariants;

    private Dictionary<Enemy, int> _currentSpawnDictionary;

    public float TimeBeforeWaveInSeconds => _timeBeforeWaveInSeconds;

    public void StartWaveSpawn()
    {
        _currentSpawnDictionary = new Dictionary<Enemy, int>();

        foreach (var enemyVariant in _enemyVariants)
        {
            _currentSpawnDictionary[enemyVariant.Enemy] = enemyVariant.Amount;
        }
    }

    public (Enemy enemy, bool isMoreToSpawn) GetNextEnemyToSpawn()
    {
        var leftToSpawn = GetEnemiesLeftToSpawn();
        var enemyChooseValue = Random.Range(0, leftToSpawn);

        var enemyToSpawn = GetEnemyByGeneralIndex(enemyChooseValue);
        var isMoreToSpawn = leftToSpawn > 1;

        _currentSpawnDictionary[enemyToSpawn]--;

        return (enemyToSpawn, isMoreToSpawn);
    }

    private Enemy GetEnemyByGeneralIndex(int index)
    {
        var additionalLegth = 0;

        foreach (var enemy in _currentSpawnDictionary.Keys)
        {
            if (index < _currentSpawnDictionary[enemy] + additionalLegth)
                return enemy;
            else
                additionalLegth += _currentSpawnDictionary[enemy];
        }

        return _currentSpawnDictionary.Keys.Last();
    }

    private int GetEnemiesLeftToSpawn()
    {
        var leftToSpawn = 0;

        foreach (var enemy in _currentSpawnDictionary.Keys)
        {
            leftToSpawn += _currentSpawnDictionary[enemy];
        }

        return leftToSpawn;
    }
}