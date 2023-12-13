using UnityEngine.InputSystem;

namespace Assets.Units.Player
{
    public class PlayerActivateSkills : InputPlayer
    {
        private PlayerUnit _player;
        private InputAction _actionStealth;

        protected override void Awake()
        {
            base.Awake();
            _actionStealth = InputSystem.Player.Stealth;
            _player = GetComponent<PlayerUnit>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _actionStealth.performed += ActivateStealth;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _actionStealth.performed -= ActivateStealth;
        }

        private void ActivateStealth(InputAction.CallbackContext context)
        {
            _player.ActivateStealth();
        }
    }
}