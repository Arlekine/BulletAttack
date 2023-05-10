using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Player _player;
    [SerializeField] private float _lerpParameter;

    [Space] 
    [SerializeField] private Vector3 _weaponRotation;
    [SerializeField] private Vector3 _weaponOffset;


    private Vector3 _standartRotation;
    private Vector3 _standartOffset;
    private Vector3 _offset;

    private void Start()
    {
        _standartOffset = _offset = transform.position - _target.position;
        _standartRotation = transform.eulerAngles;

        _player.PlayerController.GotToWeapon += OnGotToWeapon;
        _player.PlayerController.OutOfWeapon += OnOutOfWeapon;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _lerpParameter * Time.deltaTime);
    }

    private void OnGotToWeapon()
    {
        transform.DORotate(_weaponRotation, 0.5f);
        _offset = _weaponOffset;
    }

    public void OnOutOfWeapon()
    {
        transform.DORotate(_standartRotation, 0.5f);
        _offset = _standartOffset;
    }
}
