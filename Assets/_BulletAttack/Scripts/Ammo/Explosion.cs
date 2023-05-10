using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector3 _point;
    private float _radius;
    private float _force;
    private LayerMask _layerMask;
    private int _damage;

    public void Init(Vector3 point, float radius, float force, int damage, LayerMask layerMask)
    {
        _point = point;
        _radius = radius;
        _force = force;
        _layerMask = layerMask;
        _damage = damage;

        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        var colliders = Physics.OverlapSphere(_point, _radius, _layerMask);

        foreach (var collider in colliders)
        {
            var health = collider.GetComponent<Health>();

            if (health != null)
            {
                health.Hit(_damage);
            }
        }

        yield return null; 

        colliders = Physics.OverlapSphere(_point, _radius);

        foreach (var collider in colliders)
        {
            if (collider.attachedRigidbody != null && collider.attachedRigidbody.isKinematic == false)
            {
                collider.attachedRigidbody.AddExplosionForce(_force, _point, _radius);
            }
        }
    }
}
