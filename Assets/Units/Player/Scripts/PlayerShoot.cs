using System.Collections.Generic;
using Assets.Units.Player.Base;
using Assets.Units.Player.ProjectileAttack;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerShoot : InputPlayer
{
    [Header("Weapon")] 
    [SerializeField] Weapons _weapons;
    [SerializeField, Min(0)] private int _currentWeapon;

    private InputAction _actionMove;
    private Vector3 _drivingDirections = Vector3.zero;
    private List<AttackBehaviour> _weaponList = new();

    protected override void Awake()
    {
        base.Awake();
        _actionMove = InputSystem.Player.Shoot;
    }

    private void Start()
    {
        _weaponList = _weapons.GetWeapons();
        if (_weaponList.Count == 0)
        {
            Debug.LogError("_weponList == 0");
            return;
        }
        _weaponList[_currentWeapon].gameObject.SetActive(true);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    private void FixedUpdate()
    {
        _drivingDirections = _actionMove.ReadValue<Vector2>();
        _drivingDirections = GetDirection(_drivingDirections);

        RotateCharacter(_drivingDirections);
    }

    protected override void RotateCharacter(Vector3 moveDirection)
    {
        base.RotateCharacter(moveDirection);
        if (moveDirection != Vector3.zero && _weaponList.Count != 0)
        {
            _weaponList[_currentWeapon].PerformAttack();
        }
    }
}
