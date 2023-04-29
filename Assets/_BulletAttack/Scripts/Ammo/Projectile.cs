using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _maxLifetime;
    [SerializeField] protected Rigidbody _rigidbody;

    private Vector3 _direction;
    private float _creationTime;

    public void SetMoveDirection(Vector3 direction)
    {
        _creationTime = Time.time;
        _direction = direction;
        transform.forward = _direction;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _direction * _speed * Time.fixedDeltaTime);

        if (Time.time - _creationTime >= _maxLifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();

        if (health != null)
        {
            health.Hit(_damage);
            Destroy(gameObject);
        }
    }
}