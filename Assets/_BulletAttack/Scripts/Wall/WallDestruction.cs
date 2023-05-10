using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestruction : MonoBehaviour
{
    [SerializeField] private WallDestructionElement[] _wallElements;
    [SerializeField] private Health _wallHealth;
    [Range(0f, 1f)][SerializeField] private float _damagedMoment;

    private void Start()
    {
        _wallHealth.OnHit += OnHitted;
        _wallHealth.OnDead += OnDead;
    }

    private void OnHitted(Health health)
    {
        if (health.CurrentHealth < health.StartHealth * 0.5f)
        {
            foreach (var wallDestructionElement in _wallElements)
            {
                wallDestructionElement.SetDamaged();
            }
        }
    }

    private void OnDead(Health health)
    {
        foreach (var wallDestructionElement in _wallElements)
        {
            wallDestructionElement.Destroy();
        }
    }
}