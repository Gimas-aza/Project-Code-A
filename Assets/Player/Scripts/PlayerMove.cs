using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Header("Moveming")]
    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedRotate;

    private CharacterController _controller;
    private Vector3 _drivingDirections = Vector3.zero;
    private InputSystem _inputSystem;
    private InputAction _actionMove;

    private void Awake()
    {
        _inputSystem = new InputSystem();
        _actionMove = _inputSystem.Player.Move;
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _inputSystem.Enable();
        _controller.enabled = true;
    }

    private void OnDisable()
    {
        _inputSystem.Disable();
        _controller.enabled = false;
    }

    private void FixedUpdate()
    {
        _drivingDirections = _actionMove.ReadValue<Vector2>();
        _drivingDirections = new Vector3(-_drivingDirections.y, 0, _drivingDirections.x);

        MoveCharacter(_drivingDirections);
        RotateCharacter(_drivingDirections);
    }

    private void MoveCharacter(Vector3 moveDirection)
    {
        _controller.Move(moveDirection * _speedMove * Time.deltaTime);
    }

    private void RotateCharacter(Vector3 moveDirection)
    {            
        if (Vector3.Angle(transform.forward, -moveDirection) > 0)
        {                
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, -moveDirection, _speedRotate, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
