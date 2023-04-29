using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const string AnimatorMoving = "IsMoving";

    [SerializeField] private float _speed;

    [Space]
    [SerializeField] private Health _health;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;

    private Health _currentTarget;
    private Vector3 _runDirection;

    public Health Health => _health;

    private void Start()
    {
        _health.OnDead += OnDead;
    }

    private void OnDead(Health health)
    {
        gameObject.SetActive(false);
    }

    [EditorButton]
    public void SetTarget(Health health, Vector3 runDirection)
    {
        _currentTarget = health;
        _runDirection = runDirection;
    }

    private void Update()
    {
        if (_currentTarget != null)
        {
            _animator.SetBool(AnimatorMoving, true);

            transform.forward = _runDirection.normalized;
            _characterController.Move(_runDirection * _speed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool(AnimatorMoving, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var possibleTarget = other.GetComponent<Health>();

        if (possibleTarget != null && possibleTarget == _currentTarget)
        {
            _attacker.SetAttackTarget(_currentTarget);
            _currentTarget = null;
        }
    }
}