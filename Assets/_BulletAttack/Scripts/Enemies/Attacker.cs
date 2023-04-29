using UnityEngine;

public class Attacker : MonoBehaviour
{
    private const string AnimatorAttack_1 = "Attack_1";
    private const string AnimatorAttack_2 = "Attack_2";

    [SerializeField] private int _damage;
    [SerializeField] private float _attackPause;

    [Space]
    [SerializeField] private Animator _animator;

    private Health _target;
    private float _lastAttackTime;

    public void SetAttackTarget(Health target)
    {
        _target = target;
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

    public void ApplyDamageToCurrentTarget()
    {
        if (_target != null)
        {
            _target.Hit(_damage);
        }
    }
}