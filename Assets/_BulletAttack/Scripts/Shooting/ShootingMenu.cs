using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMenu : MonoBehaviour
{
    [SerializeField] private ShootingButton _buttonPrefab;
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _shootingPause;

    private AmmoInventory _inventory;
    private Dictionary<AmmoData, ShootingButton> _currentAmmo = new Dictionary<AmmoData, ShootingButton>();
    private AmmoData _currentShootingAmmo;
    
    [EditorButton]
    public void OpenMenu(AmmoInventory inventory)
    {
        _inventory = inventory;
        var ammoTypes = inventory.GetAllAmmos();

        foreach (var ammoData in ammoTypes)
        {
            var newButton = Instantiate(_buttonPrefab, _buttonsParent);
            newButton.Init(ammoData, inventory[ammoData]);
            newButton.OnButtonPressed += StartShooting;
            newButton.OnButtonUpressed += StopShooting;

            _currentAmmo[ammoData] = newButton;
        }
    }

    [EditorButton]
    public void CloseMenu()
    {
        foreach (var data in _currentAmmo.Keys)
        {
            Destroy(_currentAmmo[data].gameObject);
        }
        _currentAmmo.Clear();
    }

    private void StartShooting(AmmoData data)
    {
        if (_currentShootingAmmo != null)
            return;

        _currentShootingAmmo = data;
        foreach (var dat in _currentAmmo.Keys)
        {
            _currentAmmo[dat].Deactivate();
        }

        StartCoroutine(ShootingRoutine());
    }

    private void StopShooting(AmmoData data)
    {
        if (data != _currentShootingAmmo)
            return;

        StopAllCoroutines();
        _currentShootingAmmo = null;

        foreach (var dat in _currentAmmo.Keys)
        {
            _currentAmmo[dat].Activate();
        }

    }

    private IEnumerator ShootingRoutine()
    {
        while (true)
        {
            Shoot(_currentShootingAmmo);
            yield return new WaitForSeconds(_shootingPause);
        }
    }

    private void Shoot(AmmoData ammoToShoot)
    {
        _inventory.UseAmmo(ammoToShoot);
        _weapon.ShootToClosestTarget(ammoToShoot);

        _currentAmmo[ammoToShoot].UpdateAmmoCount(_inventory[ammoToShoot]);

        if (_inventory[ammoToShoot] == 0)
        {
            StopAllCoroutines();
        }
    }
}