using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string AnimatorMoving = "IsMoving";
    private const string AnimatorSpeed = "MovingSpeed";

    [SerializeField] private float _speed;

    [Space]
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private IKController _ikController;
    [SerializeField] private ShootingMenu _shootingMenu;
    [SerializeField] private AmmoInventory _inventory;
    [SerializeField] private AmmoCarrier _ammoCarrier;
    [SerializeField] private Trigger _playerTrigger;
    [SerializeField] private float _weaponImmobolityPause = 3f;

    private Transform _standartParent;
    private bool _isInWeapon;
    private float _canMoveTime;
    private bool _dontUpdate;

    public CharacterController CharacterController => _characterController;

    public event Action GotToWeapon;
    public event Action OutOfWeapon;

    private void Start()
    {
        _standartParent = transform.parent;
        _playerTrigger.OnEnter.AddListener(OnTriggered);
    }

    public void ResetPlayer()
    {
        _isInWeapon = false;

        _ikController.SetStandart();
        _ikController.Deactivate();
        StopAllCoroutines();
    }

    private void Update()
    {
        if (_dontUpdate)
            return;

        if (Time.time > _canMoveTime && (_joystick.Horizontal != 0 || _joystick.Vertical != 0))
        {
            if (_isInWeapon)
            {
                _isInWeapon = false;

                _shootingMenu.CloseMenu();
                _characterController.enabled = true;

                _ikController.SetStandart();
                _ikController.Deactivate();
                transform.parent = _standartParent;
                StopAllCoroutines();
                OutOfWeapon?.Invoke();
            }
            else
            {
                var moveVector = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);
                _animator.SetBool(AnimatorMoving, true);
                _animator.SetFloat(AnimatorSpeed, moveVector.magnitude);

                transform.forward = moveVector.normalized;
                _characterController.Move(moveVector * _speed * Time.deltaTime);
            }
        }
        else
        {
            _animator.SetBool(AnimatorMoving, false);
        }
    }
    

    private void OnTriggered(Collider other)
    {
        var weapon = other.GetComponent<Weapon>();

        if (weapon != null)
        {
            SetWeapon(weapon);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        if (_isInWeapon == false && _ammoCarrier.IsCarryingSomething == false)
        {
            _characterController.enabled = false;

            _isInWeapon = true;
            _shootingMenu.OpenMenu(_inventory);
            transform.parent = weapon.transform;
            _canMoveTime = Time.time + _weaponImmobolityPause;
            StartCoroutine(WeaponStandingRoutine(weapon));
            GotToWeapon?.Invoke();
        }

    }

    private IEnumerator WeaponStandingRoutine(Weapon weapon)
    {
        _dontUpdate = true;

        _animator.SetBool(AnimatorMoving, true);

        transform.forward = (weapon.StandPoint.position - transform.position).normalized;
        yield return transform.DOMove(weapon.StandPoint.position, 0.3f).WaitForCompletion();
        _animator.SetBool(AnimatorMoving, false);
        yield return transform.DORotate(Quaternion.LookRotation(weapon.StandPoint.forward).eulerAngles, 0.15f);
        
        _ikController.SetIKPoint(weapon.LeftHandPoint, weapon.RightHandPoint);
        _ikController.Activate();

        _dontUpdate = false;
    }
}