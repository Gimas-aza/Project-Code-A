using UnityEngine;
using UnityEngine.InputSystem;
using Assets.FSM;

namespace Assets.UI.FSM
{
    public class FsmMenu : MonoBehaviour 
    {
        [SerializeField] private GameObject _organizerMenu;
        [SerializeField] private GameObject _skillMenu;
        [SerializeField] private GameObject _inventoryMenu;
        [SerializeField] private FsmMenuParams _fsmMenuParams = new();

        private InputSystem _inputSystem;
        private InputAction _buttonSelect;
        private InputAction _buttonOrganizerMenu;
        private InputAction _buttonSkillsMenu;
        private InputAction _buttonInventoryMenu;
        private InputAction _back;
        private Fsm _fsm;

        private void Awake()
        {
            _inputSystem = new InputSystem();
            _buttonSelect = _inputSystem.Player.SelectMenu;
            _buttonOrganizerMenu = _inputSystem.Player.Organizer;
            _buttonSkillsMenu = _inputSystem.Player.Skills;
            _buttonInventoryMenu = _inputSystem.Player.Inventory;
            _back = _inputSystem.Player.Back;
        }

        private void OnEnable()
        {
            _inputSystem.Enable();
            _buttonSelect.performed += OpenSelectMenu;
            _buttonOrganizerMenu.performed += OpenOrganizerMenu;
            _buttonSkillsMenu.performed += OpenSkillsMenu;
            _buttonInventoryMenu.performed += OpenInventoryMenu;  
            _back.performed += CloseMenu;
        }

        private void OnDisable()
        {
            _inputSystem.Disable();
            _buttonSelect.performed -= OpenSelectMenu;
            _buttonOrganizerMenu.performed -= OpenOrganizerMenu;
            _buttonSkillsMenu.performed -= OpenSkillsMenu;
            _buttonInventoryMenu.performed -= OpenInventoryMenu;
            _back.performed -= CloseMenu;
        }

        private void Start()
        {
            _fsm = new Fsm();

            _fsm.AddState(new FsmStateGame(_fsm, _fsmMenuParams));
            _fsm.AddState(new FsmStateMenuSelect(_fsm, _fsmMenuParams));
            _fsm.AddState(new FsmStateOrganizer(_fsm, _fsmMenuParams, _organizerMenu));
            _fsm.AddState(new FsmStateSkills(_fsm, _fsmMenuParams, _skillMenu));
            _fsm.AddState(new FsmStateInventory(_fsm, _fsmMenuParams, _inventoryMenu));

            _fsm.SetState<FsmStateGame>();
        }

        private void Update()
        {
            _fsm.Update(); 
        }

        private void OpenSelectMenu(InputAction.CallbackContext context)
        {
            if (!_fsmMenuParams.MenuSelect.activeSelf)
                _fsm.SetState<FsmStateMenuSelect>();
            else
                _fsm.SetState<FsmStateGame>();
        }

        private void OpenOrganizerMenu(InputAction.CallbackContext context)
        {
            _fsm.SetState<FsmStateOrganizer>();
        } 

        private void OpenSkillsMenu(InputAction.CallbackContext context)
        {
            _fsm.SetState<FsmStateSkills>();
        }

        private void OpenInventoryMenu(InputAction.CallbackContext context)
        {
            _fsm.SetState<FsmStateInventory>();
        }

        private void CloseMenu(InputAction.CallbackContext context)
        {
            _fsm.SetState<FsmStateGame>();
        }
    }
}