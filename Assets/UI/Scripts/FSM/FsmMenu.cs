using UnityEngine;
using UnityEngine.InputSystem;
using Assets.FSM;

namespace Assets.UI.FSM
{
    public class FsmMenu : MonoBehaviour 
    {
        [SerializeField] private FsmMenuParams _fsmMenuParams = new();

        private InputSystem _inputSystem;
        private InputAction _skillsMenu;
        private InputAction _back;
        private Fsm _fsm;

        private void Awake()
        {
            _inputSystem = new InputSystem();
            _skillsMenu = _inputSystem.Player.Skills;
            _back = _inputSystem.Player.Back;
        }

        private void OnEnable()
        {
            _inputSystem.Enable();
            _skillsMenu.performed += OpenSkillsMenu;
            _back.performed += CloseMenu;
        }

        private void OnDisable()
        {
            _inputSystem.Disable();
            _skillsMenu.performed -= OpenSkillsMenu;
            _back.performed -= CloseMenu;
        }

        private void Start()
        {
            _fsm = new Fsm();

            _fsm.AddState(new FsmStateGame(_fsm, _fsmMenuParams));
            _fsm.AddState(new FsmStateMenuSelect(_fsm, _fsmMenuParams));

            _fsm.SetState<FsmStateGame>();
        }

        private void Update()
        {
            _fsm.Update(); 
        }

        private void OpenSkillsMenu(InputAction.CallbackContext context)
        {
            if (!_fsmMenuParams.MenuSelect.activeSelf)
                _fsm.SetState<FsmStateMenuSelect>();
            else
                _fsm.SetState<FsmStateGame>();
        }

        private void CloseMenu(InputAction.CallbackContext context)
        {
            _fsm.SetState<FsmStateGame>();
        }
    }
}