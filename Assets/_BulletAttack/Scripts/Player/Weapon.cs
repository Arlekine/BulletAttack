using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Transform _standPoint;
    [SerializeField] private Transform _leftHandPoint;
    [SerializeField] private Transform _rightHandPoint;

    private List<Health> _enemies = new List<Health>();

    public Transform StandPoint => _standPoint;
    public Transform LeftHandPoint => _leftHandPoint;
    public Transform RightHandPoint => _rightHandPoint;

    public bool HasTargets => _enemies.Count > 0;

    public void SetEnemies(List<Health> enemies)
    {
        _enemies.AddRange(enemies);

        foreach (var health in _enemies)
        {
            health.OnDead += RemoveEnemyFromList;
        }
    }

    [EditorButton]
    public void ShootToClosestTarget(AmmoData ammo)
    {
        if (_enemies.Count > 0)
        {
            var closestEnemy = GetClosestEnemy();
            var shootingDirection = ((closestEnemy.transform.position + Vector3.up * 1.5f) - transform.position).normalized;

            var rotation = Quaternion.LookRotation(shootingDirection).eulerAngles;
            rotation.x = 0;
            rotation.z = 0;

            transform.eulerAngles = rotation;

            var newProjectile = Instantiate(ammo.Projectile, _shootingPoint.position, Quaternion.identity);
            newProjectile.SetMoveDirection(((closestEnemy.transform.position + Vector3.up * 1.5f) - _shootingPoint.position).normalized);
            newProjectile.transform.parent = transform.parent;
        }
    }

    private void RemoveEnemyFromList(Health health)
    {
        _enemies.Remove(health);
    }

    private Health GetClosestEnemy()
    {
        Health closestEnemy = null;
        var closestDistance = 0f;

        foreach (var health in _enemies)
        {
            var distance = Vector3.Distance(transform.position, health.transform.position);
            if (closestEnemy == null || distance <= closestDistance)
            {
                closestEnemy = health;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }
}