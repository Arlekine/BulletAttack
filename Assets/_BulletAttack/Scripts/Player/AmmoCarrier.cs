using System.Collections.Generic;
using DG.Tweening;
using DitzelGames.FastIK;
using UnityEngine;

public class AmmoCarrier : MonoBehaviour
{
    [SerializeField] private Transform _carriedParent;
    [SerializeField] private float[] _defaultAmmoZPositions;
    [SerializeField] private float[] _bigAmmoZPositions;

    [Space]
    [SerializeField] private float _toPositionMoveTime;

    [Space] 
    [SerializeField] private IKController _ikController;
 
    private float _currentHeight;
    private int _currentRowIndex;

    private List<CarriedAmmo> _carriedAmmos = new List<CarriedAmmo>();

    public bool IsCarryingSomething => _carriedAmmos.Count > 0;

    private void Start()
    {
        _ikController.Deactivate();
    }

    public void Place(CarriedAmmo ammo)
    {
        ammo.MoveToCarriedPosition(_carriedParent, new Vector3(_currentHeight, 0f, _defaultAmmoZPositions[_currentRowIndex]), _toPositionMoveTime);

        _currentRowIndex++;
        if (_currentRowIndex >= _defaultAmmoZPositions.Length)
        {
            _currentRowIndex = 0;
            _currentHeight += ammo.VerticalSize;
        }

        _carriedAmmos.Add(ammo);

        if (_carriedAmmos.Count == 1)
        {
            _ikController.Activate();
        }
    }

    public void PutAllToWeapon(Transform weapon)
    {
        float offset = 0f;

        for (int i = _carriedAmmos.Count - 1; i >= 0; i--)
        {
            _carriedAmmos[i].MoveToWeaponPosition(weapon, _toPositionMoveTime, offset);
            offset += 0.02f;
        }

        _carriedAmmos.Clear();

        _ikController.Deactivate();

        _currentHeight = 0;
        _currentRowIndex = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        var collectingPoint = other.GetComponent<AmmoCollectingPoint>();

        if (collectingPoint != null)
        {
            PutAllToWeapon(collectingPoint.AmmoCollectionPoint);
        }
    }
}