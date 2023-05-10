using System;
using UnityEngine;

[Serializable]
public class EnemiesSpawnZone
{
    private const float AdditionalDistanceBetweenLines = 0.5f;
    
    [SerializeField] private float _zoneWidth;
    [SerializeField] private RandomRange _xStep;
    [SerializeField] private RandomRange _zStep;

    private Vector3 _nextSpawnPosition;
    private float _currentMiddleZ;
    private Transform _startCenter;

    private float DistanceBetweenLines => _zStep.Length + AdditionalDistanceBetweenLines;

    private bool IsNextSpawnPointIsOutOfZone => _nextSpawnPosition.x >= _startCenter.position.x + _zoneWidth * 0.5f;

    public void SetZoneCenter(Transform zoneCenter)
    {
        _startCenter = zoneCenter;
    }

    public void StartNewSpawn()
    {
        _currentMiddleZ = _startCenter.position.z;

        _nextSpawnPosition = _startCenter.position;
        _nextSpawnPosition.x -= _zoneWidth * 0.5f;
    }

    public Vector3 GetNextSpawnPosition()
    {
        _nextSpawnPosition.x += _xStep.GetValue();
        _nextSpawnPosition.z = _currentMiddleZ + _zStep.GetValue();

        if (IsNextSpawnPointIsOutOfZone)
        {
            _currentMiddleZ += DistanceBetweenLines;
            _nextSpawnPosition.x = _startCenter.position.x - _zoneWidth * 0.5f;
        }

        return _nextSpawnPosition;
    }
}