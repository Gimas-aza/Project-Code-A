using System;
using Assets.Units.Base;
using Assets.Units.ProjectileAttack;
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
        [SerializeField] private Transform _targetTransform;
        [SerializeField] public float _angleInDegrees;

        private InputAction _actionShoot;
        private InputAction _actionSwitchWeapon;
        private Vector3 _drivingDirections = Vector3.zero;
        private AttackBehaviour _currentWeapon;
        private ProjectileAttackWeapon _currentProjectileAttack;
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
            _currentProjectileAttack = _currentWeapon.GetComponent<ProjectileAttackWeapon>();

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
            if (_currentWeapon != null)
                _currentProjectileAttack = _currentWeapon.GetComponent<ProjectileAttackWeapon>();
        }

        protected override void RotateCharacter(Vector3 moveDirection)
        {
            float force = 0;
            if (_currentProjectileAttack != null && _currentProjectileAttack.BulletFlightType == BulletFlightType.Parabolic)
            {
                _currentWeapon.transform.localEulerAngles = new Vector3(-_angleInDegrees, 0f, 0f);
                force = GetParabola();
            }
            else
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
                _currentWeapon.PerformAttack(force);
                _directionShoot = Vector3.zero;
                _laserPointer.SetActivateLaser(false);
            }
        }

        private float GetParabola()
        {
            float g = Physics.gravity.y;
            Vector3 fromTo = _targetTransform.position - _currentWeapon.transform.position;
            Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

            transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

            float x = fromToXZ.magnitude;
            float y = fromTo.y;

            float AngleInRadians = _angleInDegrees * Mathf.PI / 180;

            float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
            float v = Mathf.Sqrt(Mathf.Abs(v2));
            Debug.Log(v);
            return v;
        }
    }
}