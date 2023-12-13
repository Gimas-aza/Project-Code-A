using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Units.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : InputPlayer
    {
        [Header("Moveming")]
        [SerializeField] private float _speedMove;

        private float _beginSpeedMove;
        private float _multiplierSpeed = 1.5f;
        private InputAction _actionMove;
        private Vector3 _drivingDirections = Vector3.zero;
        private CharacterController _controller;

        public bool IsMove { get; private set; } = false;

        protected override void Awake()
        {
            base.Awake();
            _beginSpeedMove = _speedMove;
            _controller = GetComponent<CharacterController>();
            _actionMove = InputSystem.Player.Move;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _controller.enabled = true;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _controller.enabled = false;
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
            _controller.Move(moveDirection * _speedMove * Time.deltaTime);
            
            IsMove = false;
            if (moveDirection != Vector3.zero)
                IsMove = true;
        }

        public void LesserSpeed(bool isActive)
        {
            if (isActive)
                _speedMove = _beginSpeedMove / _multiplierSpeed;
            else
                _speedMove = _beginSpeedMove;
        }
    }
}