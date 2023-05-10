using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieSpawner : MonoBehaviour
{
    public Action<Enemy> EnemyDead;
    public Action WavesCleared;
    public Action WaveSpawned;

    [SerializeField] private WavesView _waveUI;
    [SerializeField] private EnemiesSpawnZone _zone;
    [SerializeField] private float _timeBetweenWavesInSeconds = 3f;

    private float _slowdown;
    private bool _isLastWave;

    private Transform _attackDirection;
    private Health _wall;
    private Weapon _weapon;

    private List<Enemy> _currentEnemies = new List<Enemy>();

    public void SetWaves(EnemyWave[] waves, Transform attackPoint, Health wall, Weapon weapon)
    {
        _attackDirection = attackPoint;
        _wall = wall;
        _weapon = weapon;

        _waveUI.SetStandart();
        _waveUI.gameObject.SetActive(true);
        _zone.SetZoneCenter(attackPoint);
        StartCoroutine(WavesRoutine(waves));
    }

    [EditorButton]
    public void SpawnWave(EnemyWave wave)
    {
        _zone.StartNewSpawn();
        var enemiesHealth = new List<Health>();

        wave.StartWaveSpawn();

        var enemyToSpawn = wave.GetNextEnemyToSpawn();

        while (enemyToSpawn.isMoreToSpawn)
        {
            var spawnPos = _zone.GetNextSpawnPosition();
            var enemy = Instantiate(enemyToSpawn.enemy, spawnPos, Quaternion.identity);
            enemy.transform.parent = _attackDirection;

            enemy.transform.forward = _attackDirection.forward;
            enemy.SetTarget(_wall, _attackDirection.forward);
            enemy.Slow(_slowdown);

            enemiesHealth.Add(enemy.Health); 
            enemyToSpawn = wave.GetNextEnemyToSpawn();


            enemy.Dead += OnEnemyDead;
            _currentEnemies.Add(enemy);
        }

        _weapon.SetEnemies(enemiesHealth);
        WaveSpawned?.Invoke();
    }

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.Dead -= OnEnemyDead;
        EnemyDead?.Invoke(enemy);
        _currentEnemies.Remove(enemy);

        if (_isLastWave && _currentEnemies.Count == 0)
            WavesCleared?.Invoke();
    }

    public void SetSlowdown(float slowdown)
    {
        if (slowdown < 0)
            throw new ArgumentException($"{nameof(slowdown)} should be positive");

        _slowdown += slowdown;
    }

    public void StopSpawing()
    {
        _isLastWave = false;
        _waveUI.SetStandart();
        _waveUI.gameObject.SetActive(false);
        _currentEnemies.Clear();
        StopAllCoroutines();
    }

    private IEnumerator WavesRoutine(EnemyWave[] waves)
    {
        for (int i = 0; i < waves.Length; i++)
        {
            _waveUI.SetWave(i, waves.Length);
            var currentTime = 0f;
            while (currentTime < waves[i].TimeBeforeWaveInSeconds)
            {
                var remainingTime = waves[i].TimeBeforeWaveInSeconds - currentTime;
                _waveUI.UpdateTimer(remainingTime, remainingTime / waves[i].TimeBeforeWaveInSeconds);
                yield return null;
                currentTime += Time.deltaTime;
            }
            
            //show wave incoming UI

            yield return new WaitForSeconds(_timeBetweenWavesInSeconds);

            SpawnWave(waves[i]);
        }

        _waveUI.SetWave(waves.Length, waves.Length);
        _waveUI.SetFinalWave();
        _isLastWave = true;
    }
}