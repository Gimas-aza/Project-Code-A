using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayer : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float _speedRotate;

    protected CharacterController Controller;
    protected InputSystem InputSystem;

    protected virtual void Awake()
    {
        InputSystem = new InputSystem();
        Controller = GetComponent<CharacterController>();
    }

    protected virtual void OnEnable()
    {
        InputSystem.Enable();
        Controller.enabled = true;
    }

    protected virtual void OnDisable()
    {
        InputSystem.Disable();
        Controller.enabled = false;
    }

    protected virtual void RotateCharacter(Vector3 moveDirection)
    {            
        if (Vector3.Angle(transform.forward, -moveDirection) > 0)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, -moveDirection, _speedRotate, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
