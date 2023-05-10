using System;
using System.Collections;
using UnityEngine;

public class WallDestructionElement : MonoBehaviour
{
    [Serializable]
    private class DestructionPart
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Rigidbody _rigidbody;

        public void Activate(Material material)
        {
            _meshRenderer.gameObject.SetActive(true);
            _meshRenderer.material = material;
            _rigidbody.isKinematic = false;
        }

        public void Deactive()
        {
            _meshRenderer.gameObject.SetActive(false);
        }
    }

    [SerializeField] private MeshRenderer _fullMeshRenderer;
    [SerializeField] private Material _damagedMaterial;
    [SerializeField] private DestructionPart[] _destructionParts;
    [SerializeField] private Transform[] _detanationPoints;

    private void Start()
    {
        foreach (var destructionPart in _destructionParts)
        {
            destructionPart.Deactive();
        }
    }

    public void SetDamaged()
    {
        _fullMeshRenderer.material = _damagedMaterial;
    }

    public void Destroy()
    {
        _fullMeshRenderer.enabled = false;
        foreach (var destructionPart in _destructionParts)
        {
            destructionPart.Activate(_damagedMaterial);
        }

        StartCoroutine(DestructRoutine());
    }

    private IEnumerator DestructRoutine()
    {
        yield return null; 
        yield return null; 
        
        foreach (var detanationPoint in _detanationPoints)
        {
            var colliders = Physics.OverlapSphere(detanationPoint.position, 0.5f);

            foreach (var collider in colliders)
            {
                var body = collider.GetComponent<Rigidbody>();
                if (body != null && body.isKinematic == false)
                {
                    body.AddExplosionForce(10000, detanationPoint.position, 0.5f);
                }
            }
        }
    }
}