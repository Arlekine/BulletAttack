using UnityEngine;

public class ShotgunProjectile : Projectile
{
    [Space]
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _maxTargetHitted;
    [SerializeField] private float _startRadius;
    [SerializeField] private float _endRadius;

    private int _currentTargetHitted;

    protected override void FixedUpdate()
    {
        var timeProgress = 1f - ((_maxLifetime - (Time.time - _creationTime)) / _maxLifetime);
        _collider.radius = Mathf.Lerp(_startRadius, _endRadius, timeProgress);
        _damage = (int)Mathf.Lerp(_maxDamage, _minDamage, timeProgress);

        base.FixedUpdate();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();

        if (health != null)
        {
            OnTargetHitted(health);
            _currentTargetHitted++;
            
            if (_currentTargetHitted >= _maxTargetHitted)
                Destroy(gameObject);
        }
    }
}