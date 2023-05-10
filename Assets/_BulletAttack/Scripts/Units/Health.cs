using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action<Health> OnDead;
    public Action<Health> OnHit;

    private const int MinHealth = 0;
    
    [SerializeField]
    private int _startHealth = 100;


    [SerializeField] private int _currentHealth;

    public int CurrentHealth => _currentHealth;
    public int StartHealth => _startHealth;
    public float HealthNormalized => (float)_currentHealth / (float)_startHealth;

    public bool IsDead { get; private set; }



    private void Awake()
    {
        _currentHealth = _startHealth;
    }

    [EditorButton]
    public void Hit(int damage)
    {
        if (damage <= 0)
            throw new ArgumentException($"{nameof(damage)} should be positive value");

        _currentHealth -= damage;

        OnHit?.Invoke(this);
        if (_currentHealth <= MinHealth)
        {
            _currentHealth = MinHealth;
            Death();
        }
    }

    [EditorButton]
    public void Die()
    {
        _currentHealth = MinHealth;
        Death();
    }

    protected virtual void Death()
    {
        if (IsDead) return;

        IsDead = true;
        OnDead?.Invoke(this);
    }

    private void Revive()
    {
        _currentHealth = _startHealth;
        IsDead = false;
    }
}
