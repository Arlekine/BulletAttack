using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DitzelGames.FastIK;
using UnityEngine;

public class AmmoCarrier : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _carriedParent;
    [SerializeField] private float[] _defaultAmmoZPositions;
    [SerializeField] private float[] _bigAmmoZPositions;

    [Space]
    [SerializeField] private float _toPositionMoveTime;

    [Space] 
    [SerializeField] private IKController _ikController;
    [SerializeField] private Transform _leftHandCarry;
    [SerializeField] private Transform _rightHandCarry;

    private float _currentHeight;
    private int _currentRowIndex;
    private AmmoCollectingPoint _currentWeapon;

    private AmmoCollectingPoint _currentAmmoCollectingPoint;

    private List<CarriedAmmo> _carriedAmmos = new List<CarriedAmmo>();

    public bool IsCarryingSomething => _carriedAmmos.Count > 0;

    private void Start()
    {
        _ikController.Deactivate();
    }

    public void Clear()
    {
        StopAllCoroutines();
        foreach (var carriedAmmo in _carriedAmmos)
        {
            Destroy(carriedAmmo.gameObject);
        }

        if (_currentWeapon != null)
            _currentWeapon.StopCollecting();

        _currentHeight = 0;
        _currentRowIndex = 0;

        _carriedAmmos.Clear();
    }

    public void Place(CarriedAmmo ammo)
    {
        if (_currentRowIndex >= _defaultAmmoZPositions.Length)
        {
            _currentRowIndex = 0;
            _currentHeight += _carriedAmmos.Last().HalfVerticalSize + ammo.HalfVerticalSize;
        }

        ammo.MoveToCarriedPosition(_carriedParent, new Vector3(_currentHeight, 0f, _defaultAmmoZPositions[_currentRowIndex]), _toPositionMoveTime);

        _carriedAmmos.Add(ammo);
        _currentRowIndex++;

        if (_carriedAmmos.Count == 1)
        {
            _ikController.Activate();
            _ikController.SetIKPoint(_leftHandCarry, _rightHandCarry, true);
        }
    }

    public IEnumerator PutAllToWeaponRoutine(AmmoCollectingPoint weapon)
    {
        while (_carriedAmmos.Count > 0)
        {
            _carriedAmmos[^1].MoveToWeaponPosition(weapon.AmmoCollectionPoint, _toPositionMoveTime);

            _currentRowIndex--;

            if (_currentRowIndex < 0)
            {
                _currentRowIndex = _defaultAmmoZPositions.Length - 1;
                _currentHeight -= _carriedAmmos[^1].VerticalSize;
            }

            _carriedAmmos.RemoveAt(_carriedAmmos.Count - 1);

            yield return new WaitForSeconds(0.02f);
        }

        _player.PlayerController.SetWeapon(weapon.Weapon);
        weapon.StopCollecting();
        _ikController.Deactivate();
        _ikController.SetStandart();

        _currentHeight = 0;
        _currentRowIndex = 0;
        _currentWeapon = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        var collectingPoint = other.GetComponent<AmmoCollectingPoint>();

        if (collectingPoint != null && _currentAmmoCollectingPoint == null && IsCarryingSomething)
        {
            _currentWeapon = collectingPoint;
            collectingPoint.StartCollecting(_toPositionMoveTime);
            _currentAmmoCollectingPoint = collectingPoint;
            StartCoroutine(PutAllToWeaponRoutine(collectingPoint));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var collectingPoint = other.GetComponent<AmmoCollectingPoint>();

        if (collectingPoint != null && _currentAmmoCollectingPoint == collectingPoint)
        {
            _currentWeapon = null;
            collectingPoint.StopCollecting();
            _currentAmmoCollectingPoint = null;
            StopAllCoroutines();
        }
    }
}