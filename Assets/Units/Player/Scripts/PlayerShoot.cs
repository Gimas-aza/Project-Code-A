using System;
using Assets.Units.Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Units.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerShoot : InputPlayer
    {
        [Header("Weapon")] 
        [SerializeField] private Weapons _weapons;
        [SerializeField] private LaserPointer _laserPointer;
        [SerializeField] private int _maxIndex = 2;
        [SerializeField, Min(0)] private int _currentIndex;
        [SerializeField] private float _speedRotate;

        private InputAction _actionShoot;
        private InputAction _actionSwitchWeapon;
        private Vector3 _drivingDirections = Vector3.zero;
        private AttackBehaviour _currentWeapon;
        private Vector3 _directionShoot;

        private void OnValidate()
        {
            _weapons ??= GetComponentInChildren<Weapons>();
            _currentIndex = Mathf.Clamp(_currentIndex, 0, _maxIndex);
        }

        protected override void Awake()
        {
            base.Awake();
            _actionShoot = InputSystem.Player.Shoot;
            _actionSwitchWeapon = InputSystem.Player.SwitchWeapon;

            _weapons.Init();
            _currentWeapon = _weapons.GetWeapon(_currentIndex);

            SpeedRotate = _speedRotate;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _actionSwitchWeapon.performed += SwitchWeapon;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _actionSwitchWeapon.performed -= SwitchWeapon;
        }

        private void FixedUpdate()
        {
            _drivingDirections = _actionShoot.ReadValue<Vector2>();
            _drivingDirections = GetDirection(_drivingDirections);

            RotateCharacter(_drivingDirections);
        }

        private void SwitchWeapon(InputAction.CallbackContext context)
        {
            var maxIndex = Math.Clamp(_weapons.GetCountWeapons() - 1, 0, _maxIndex);
            var index = (_currentIndex + 1 > maxIndex) ? 0 : _currentIndex + 1;
            _currentIndex = index;

            _currentWeapon = _weapons.GetWeapon(_currentIndex);
        }

        protected override void RotateCharacter(Vector3 moveDirection)
        {
            base.RotateCharacter(moveDirection);
            if (_currentWeapon == null)
                return;

            if (moveDirection != Vector3.zero)
            {
                _directionShoot = moveDirection;
                _laserPointer.SetActivateLaser(true);
            }
            
            if (moveDirection == Vector3.zero && _directionShoot != Vector3.zero)
            {
                _currentWeapon.PerformAttack();
                _directionShoot = Vector3.zero;
                _laserPointer.SetActivateLaser(false);
            }

            // if (moveDirection != Vector3.zero && _currentWeapon != null)
            // {
            //     _currentWeapon.PerformAttack();
            // }
        }
    }
}