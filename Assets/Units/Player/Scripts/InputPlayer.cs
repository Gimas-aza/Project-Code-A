using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Units.Player
{
    public class InputPlayer : MonoBehaviour
    {
        protected float SpeedRotate = 0.2f;
        protected InputSystem InputSystem;
        protected Camera MainCamera;

        [Inject]
        private void Constructor(Camera mainCamera, InputSystem inputSystem)
        {
            MainCamera = mainCamera;
            InputSystem = inputSystem;
        }

        protected virtual void Awake()
        {
        }

        protected virtual void OnEnable()
        {
            InputSystem.Player.Enable();
        }

        protected virtual void OnDisable()
        {
            InputSystem.Player.Disable();
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
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, SpeedRotate, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
    }
}