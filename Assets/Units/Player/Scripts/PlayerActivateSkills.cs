using UnityEngine.InputSystem;

namespace Assets.Units.Player
{
    public class PlayerActivateSkills : InputPlayer
    {
        private PlayerUnit _player;
        private InputAction _actionStealth;
        private InputAction _actionShield;

        protected override void Awake()
        {
            base.Awake();
            _actionStealth = InputSystem.Player.Stealth;
            _actionShield = InputSystem.Player.Shield;
            _player = GetComponent<PlayerUnit>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _actionStealth.performed += ActivateStealth;
            _actionShield.performed += ActivateShield;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _actionStealth.performed -= ActivateStealth;
            _actionShield.performed -= ActivateShield;
        }

        private void ActivateStealth(InputAction.CallbackContext context) => 
            _player.ActivateStealth();

        private void ActivateShield(InputAction.CallbackContext context) =>
            _player.ActiveShield();
    }
}