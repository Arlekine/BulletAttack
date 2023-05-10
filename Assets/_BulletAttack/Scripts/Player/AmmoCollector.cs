using System;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollector : MonoBehaviour
{
    public Action<AmmoData> AmmoCollected;

    [SerializeField] private AmmoInventory _ammoInventory;
    [SerializeField] private AmmoCarrier _ammoCarrier;
    [SerializeField] private CapsuleCollider _collectionCollider;
    
    [SerializeField] private float _additionalSpeed;

    private List<CollectableAmmo> _currentCollectables = new List<CollectableAmmo>();
    private float _lastSoundPlay;

    private void OnTriggerEnter(Collider other)
    {
        var collectable = other.GetComponent<CollectableAmmo>();

        if (collectable != null && _currentCollectables.Contains(collectable) == false)
        {
            _currentCollectables.Add(collectable);
            collectable.StartCollecting(1f + _additionalSpeed);
            collectable.Collected += OnAmmoCollected;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var collectable = other.GetComponent<CollectableAmmo>();

        if (collectable != null && _currentCollectables.Contains(collectable))
        {
            collectable.Collected -= OnAmmoCollected;
            collectable.StopCollecting();
            _currentCollectables.Remove(collectable);
        }
    }

    private void OnAmmoCollected(CollectableAmmo ammo)
    {
        if (Time.time - _lastSoundPlay > 0.1f)
        {
            _lastSoundPlay = Time.time;
            SoundManager.Instance.Collect.Play();
        }

        ammo.Collected -= OnAmmoCollected;
        _currentCollectables.Remove(ammo);
        _ammoInventory.AddAmmo(ammo.AmmoData);
        var carriedAmmo = Instantiate(ammo.AmmoData.CarriedAmmo, ammo.ModelPosition, ammo.ModelRotation);
        _ammoCarrier.Place(carriedAmmo); 
        AmmoCollected?.Invoke(ammo.AmmoData);
    }

    public void AddRange(float range)
    {
        if (range <= 0)
            throw new ArgumentException($"{nameof(range)} should be positive");
        
        _collectionCollider.radius += range;
    }

    public void AddSpeed(float speedModifier)
    {
        if (speedModifier <= 0)
            throw new ArgumentException($"{nameof(speedModifier)} should be positive");

        _additionalSpeed += speedModifier;
    }
}