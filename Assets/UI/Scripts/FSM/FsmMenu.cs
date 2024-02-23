using UnityEngine;
using UnityEngine.InputSystem;
using Assets.FSM;
using Zenject;

namespace Assets.UI.FSM
{
    public class FsmMenu : MonoBehaviour 
    {
        [SerializeField] private GameObject _organizerMenu;
        [SerializeField] private GameObject _skillMenu;
        [SerializeField] private GameObject _inventoryMenu;
        [SerializeField] private RadioGame _hackMenu;
        [SerializeField] private FsmMenuParams _fsmMenuParams = new();

        private InputSystem _inputSystem;
        private InputAction _buttonSelect;
        private InputAction _buttonOrganizerMenu;
        private InputAction _buttonSkillsMenu;
        private InputAction _buttonInventoryMenu;
        private InputAction _back;
        private Fsm _fsm;

        [Inject]
        private void Constructor(InputSystem inputActions)
        {
            _inputSystem = inputActions;
            _fsmMenuParams.SetInputSystem(_inputSystem);
        }

        private void Awake()
        {
            _buttonSelect = _inputSystem.OpenMenu.SelectMenu;
            _buttonOrganizerMenu = _inputSystem.OpenMenu.Organizer;
            _buttonSkillsMenu = _inputSystem.OpenMenu.Skills;
            _buttonInventoryMenu = _inputSystem.OpenMenu.Inventory;
            _back = _inputSystem.Menu.Back;
        }

        private void OnEnable()
        {
            _inputSystem.OpenMenu.Enable();
            _buttonSelect.performed += OpenSelectMenu;
            _buttonOrganizerMenu.performed += OpenOrganizerMenu;
            _buttonSkillsMenu.performed += OpenSkillsMenu;
            _buttonInventoryMenu.performed += OpenInventoryMenu;  
            _back.performed += CloseMenu;
        }

        private void OnDisable()
        {
            _inputSystem.OpenMenu.Disable();
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
            _fsm.AddState(new FsmStateHack(_fsm, _fsmMenuParams, _hackMenu));

            _fsm.SetState<FsmStateGame>();
        }

        private void Update()
        {
            _fsm.Update(); 
        }

        private void OpenSelectMenu(InputAction.CallbackContext context)
        {
            if (!_fsmMenuParams.MenuSelect.activeSelf)
            {
                _fsm.SetState<FsmStateMenuSelect>();
            }
            else
            {
                _fsm.SetState<FsmStateGame>();
            }
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

        [ContextMenu(nameof(OpenHackMenu))]
        private void OpenHackMenu()
        {
            _fsm.SetState<FsmStateHack>();
        }

        private void CloseMenu(InputAction.CallbackContext context)
        {
            _fsm.SetState<FsmStateGame>();
            Debug.Log("Close Menu");
        }

        [ContextMenu(nameof(EnableMenuInput))]
        private void EnableMenuInput()
        {
            _inputSystem.Player.Disable();
            _inputSystem.Menu.Enable();
        }

        [ContextMenu(nameof(EnablePlayerInput))]
        private void EnablePlayerInput()
        {
            _inputSystem.Menu.Disable();
            _inputSystem.Player.Enable();
        }
    }
}