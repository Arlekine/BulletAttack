using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> Dead;

    private const string AnimatorMoving = "IsMoving";
    private const string AnimatorMovingSpeed = "MovingSpeed";
    private const string AnimatorMovingOffset = "MoveOffset";

    private const float SpeedToAnimatorSpeedParameter = 3f;

    [Min(0.1f)][SerializeField] private float _speed;
    [Min(0.1f)][SerializeField] private float _minSpeed;
    [Min(0.1f)][SerializeField] private int _currency;
    [Min(0.5f)][SerializeField] private float _corpseDisappearOffsetInSeconds = 3f;

    [Space]
    [SerializeField] private Health _health;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Rigidbody[] _ragdoll;

    private Health _currentTarget;
    private Vector3 _runDirection;

    public Health Health => _health;
    public int Currency => _currency;

    private void Start()
    {
        _health.OnDead += OnDead;
        _animator.SetFloat(AnimatorMovingSpeed, _speed / SpeedToAnimatorSpeedParameter);
        _animator.SetFloat(AnimatorMovingOffset, Random.Range(0f, 1f));
    }

    private void OnDead(Health health)
    {
        Destroy(_animator);
        Destroy(_attacker);
        Destroy(GetComponent<Collider>());
        Destroy(_rigidbody);
        Destroy(this);
        Destroy(gameObject, _corpseDisappearOffsetInSeconds);

        foreach (var body in _ragdoll)
        {
            body.isKinematic = false;
        }

        Dead?.Invoke(this);
    }

    public void Slow(float speedToSubtract)
    {
        if (speedToSubtract < 0)
            throw new ArgumentException($"{nameof(speedToSubtract)} should be positive");

        _speed = Mathf.Max(_speed - speedToSubtract, _minSpeed);
    }

    [EditorButton]
    public void SetTarget(Health health, Vector3 runDirection)
    {
        _currentTarget = health;
        _runDirection = runDirection;
    }

    private void FixedUpdate()
    {
        if (_currentTarget != null)
        {
            _animator.SetBool(AnimatorMoving, true);

            transform.forward = _runDirection.normalized;
            _rigidbody.MovePosition(_rigidbody.position + _runDirection * _speed * Time.deltaTime);
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