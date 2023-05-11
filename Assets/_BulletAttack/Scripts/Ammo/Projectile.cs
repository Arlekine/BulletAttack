using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _maxLifetime;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected ParticleSystem _hitEffect;

    protected Vector3 _direction;
    protected float _creationTime;

    public void SetMoveDirection(Vector3 direction)
    {
        _creationTime = Time.time;
        _direction = direction;
        transform.forward = _direction;
    }

    protected virtual void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _direction * _speed * Time.fixedDeltaTime);

        if (Time.time - _creationTime >= _maxLifetime)
            Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();

        if (health != null)
        {
            OnTargetHitted(health);
            Destroy(gameObject);
        }
    }

    protected virtual void OnTargetHitted(Health health)
    {
        if (_hitEffect != null)
        {
            var effect = Instantiate(_hitEffect, health.transform.position + Vector3.up, Quaternion.identity);
            effect.transform.parent = transform.parent;
        }

        health.Hit(_damage);
    }
}