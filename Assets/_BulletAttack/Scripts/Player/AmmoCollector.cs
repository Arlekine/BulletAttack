using System.Collections.Generic;
using UnityEngine;

public class AmmoCollector : MonoBehaviour
{
    [SerializeField] private AmmoInventory _ammoInventory;
    [SerializeField] private AmmoCarrier _ammoCarrier;

    private List<CollectableAmmo> _currentCollectables = new List<CollectableAmmo>();

    private void OnTriggerEnter(Collider other)
    {
        var collectable = other.GetComponent<CollectableAmmo>();

        if (collectable != null && _currentCollectables.Contains(collectable) == false)
        {
            _currentCollectables.Add(collectable);
            collectable.StartCollecting(1f);
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
        _currentCollectables.Remove(ammo);
        _ammoInventory.AddAmmo(ammo.AmmoData);
        var carriedAmmo = Instantiate(ammo.AmmoData.CarriedAmmo, ammo.ModelPosition, ammo.ModelRotation);
        _ammoCarrier.Place(carriedAmmo);
    }
}