using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string AnimatorMoving = "IsMoving";
    private const string AnimatorSitting = "IsSitting";
    private const string AnimatorSpeed = "MovingSpeed";

    [SerializeField] private float _speed;

    [Space]
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private IKController _ikController;
    [SerializeField] private ShootingMenu _shootingMenu;
    [SerializeField] private AmmoInventory _inventory;

    private Transform _standartParent;
    private bool _isInWeapon;

    private void Start()
    {
        _standartParent = transform.parent;
    }

    private void Update()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            if (_isInWeapon)
            {
                _shootingMenu.CloseMenu();
                _characterController.enabled = true;
                _isInWeapon = false;

                _ikController.SetStandart();
                _ikController.Deactivate();
                transform.parent = _standartParent;
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
    private void OnTriggerEnter(Collider other)
    {
        var weapon = other.GetComponent<Weapon>();

        if (weapon != null)
        {
            _characterController.enabled = false;
            transform.position = weapon.StandPoint.position;
            transform.forward = weapon.StandPoint.forward;

            _ikController.SetIKPoint(weapon.LeftHandPoint, weapon.RightHandPoint);
            _ikController.Activate();
            _isInWeapon = true;
            _shootingMenu.OpenMenu(_inventory);
            transform.parent = weapon.transform;
        }
    }
}