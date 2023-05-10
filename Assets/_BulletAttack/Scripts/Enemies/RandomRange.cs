using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class RandomRange
{
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;

    public float MinValue => _minValue;
    public float MaxValue => _maxValue;

    public float Length => _maxValue - _minValue;

    public float GetValue() => Random.Range(_minValue, _maxValue);
}