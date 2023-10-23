using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayer : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float _speedRotate;
    [SerializeField] protected Camera MainCamera;

    protected InputSystem InputSystem;

    protected virtual void Awake()
    {
        InputSystem = new InputSystem();
    }

    protected virtual void OnEnable()
    {
        InputSystem.Enable();
    }

    protected virtual void OnDisable()
    {
        InputSystem.Disable();
    }

    protected Vector3 GetDirection(Vector2 moveDirection)
    {
        Vector3 cameraForward = MainCamera.transform.forward;
        Vector3 cameraRight = MainCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        return (cameraRight.normalized * moveDirection.x + moveDirection.y * cameraForward.normalized).normalized;
    }

    protected virtual void RotateCharacter(Vector3 moveDirection)
    {
        if (Vector3.Angle(transform.forward, moveDirection) > 0)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _speedRotate, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}