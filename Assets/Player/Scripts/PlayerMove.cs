using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : InputPlayer
{
    [Header("Moveming")]
    [SerializeField] private float _speedMove;

    private InputAction _actionMove;
    private Vector3 _drivingDirections = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        _actionMove = InputSystem.Player.Move;
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

        MoveCharacter(_drivingDirections);
        RotateCharacter(_drivingDirections);
    }

    private void MoveCharacter(Vector3 moveDirection)
    {
        Controller.Move(moveDirection * _speedMove * Time.deltaTime);
    }
}
