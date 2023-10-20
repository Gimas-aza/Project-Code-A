using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerShoot : InputPlayer
{
    private InputAction _actionMove;
    private Vector3 _drivingDirections = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        _actionMove = InputSystem.Player.Shoot;
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
        _drivingDirections = new Vector3(-_drivingDirections.y, 0, _drivingDirections.x);

        RotateCharacter(_drivingDirections);
    }

    protected override void RotateCharacter(Vector3 moveDirection)
    {
        base.RotateCharacter(moveDirection.normalized);

    }
}
