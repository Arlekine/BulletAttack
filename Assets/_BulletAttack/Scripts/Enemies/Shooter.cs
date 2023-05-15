using System.Collections;
using UnityEngine;

public class Shooter : Attacker
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackPause;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private ParticleSystem _muzzleFire;
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private Transform _shootingPoint;

    private Health _target;

    private void Shot()
    {
        if (_target == null || _target.IsDead)
            return;

        var newProjectile = Instantiate(_projectile, _shootingPoint.position, Quaternion.identity);
        newProjectile.SetMoveDirection((_shootingPoint.forward).normalized);
        newProjectile.transform.parent = transform.parent.parent;

        var muzzle = Instantiate(_muzzleFire, _shootingPoint);
        muzzle.transform.position = _shootingPoint.position;
        muzzle.transform.forward = _shootingPoint.forward;
        
        _shotSound.Play();
    }

    public override void SetAttackTarget(Health target)
    {
        _target = target;

        if (target.IsDead)
            return;

        StartCoroutine(ShootingRoutine());
    }

    private IEnumerator ShootingRoutine()
    {
        while (_target != null && _target.IsDead == false)
        {
            yield return new WaitForSeconds(Random.Range(Mathf.Max(_attackPause - 1f, 0.1f), _attackPause + 1f));
            Shot();
        }
    }
}