using UnityEngine;

public class ExplodableProjectile : Projectile
{
    [Space] 
    [SerializeField] private Explosion _explosionPrefab;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private LayerMask _enemiesLayer;

    protected override void OnTargetHitted(Health health)
    {
        var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.parent = transform.parent;
        explosion.Init(transform.position, _explosionRadius, _explosionForce, _damage, _enemiesLayer);
    }
}