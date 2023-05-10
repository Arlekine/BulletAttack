using UnityEngine;
using Random = UnityEngine.Random;

public class Attacker : MonoBehaviour
{
    private const string AnimatorAttack_1 = "Attack_1";
    private const string AnimatorAttack_2 = "Attack_2";
    private const string AnimatorAttackSpeed = "AttackSpeed";

    [SerializeField] private int _damage;
    [SerializeField] private float _attackPause;
    [Min(0.1f)][SerializeField] private float _animationSpeed;

    [Space]
    [SerializeField] private Animator _animator;
    [SerializeField] private AttackAnimationTrigger _attackAnimationTrigger;

    private Health _target;
    private float _lastAttackTime;

    private void Start()
    {
        _animator.SetFloat(AnimatorAttackSpeed, _animationSpeed);
    }


    private void OnEnable()
    {
        _attackAnimationTrigger.Attack += ApplyDamageToCurrentTarget;
    }

    private void OnDisable()
    {
        _attackAnimationTrigger.Attack -= ApplyDamageToCurrentTarget;
    }

    private void OnDestroy()
    {
        if (_target != null)
            _target.OnDead -= StopAttack;
    }

    public void SetAttackTarget(Health target)
    {
        if (target.IsDead)
            return;

        _target = target;
        _target.OnDead += StopAttack;
    }

    private void Update()
    {
        if (_target != null)
        {
            if (Time.time > _lastAttackTime)
            {
                _lastAttackTime = Time.time + _attackPause;
                var attackTrigger = Random.Range(0f, 1f) > 0.5f ? AnimatorAttack_1 : AnimatorAttack_2;
                _animator.SetTrigger(attackTrigger);
            }
        }
    }

    private void StopAttack(Health health)
    {
        _target = null;
    }

    public void ApplyDamageToCurrentTarget()
    {
        if (_target != null)
        {
            _target.Hit(_damage);
        }
    }
}