using System.Collections;
using System.Collections.Generic;
using Assets.Units.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Units.Player
{
    public class SelectMenu : InputPlayer 
    {
        [SerializeField] private GameObject _menuSelect;
        [SerializeField] private GameObject _inputButton;
        [SerializeField] private GameObject _statisticMenu;

        private InputAction _skillsMenu;
        private InputAction _back;

        protected override void Awake()
        {
            base.Awake();
            _skillsMenu = InputSystem.Player.Skills;
            _back = InputSystem.Player.Back;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _skillsMenu.performed += OpenSkillsMenu;
            _back.performed += CloseMenu;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _skillsMenu.performed -= OpenSkillsMenu;
            _back.performed -= CloseMenu;
        }

        private void OpenSkillsMenu(InputAction.CallbackContext context)
        {
            _menuSelect.SetActive(!_menuSelect.activeSelf);
            HideInterface(!_menuSelect.activeSelf);
            SetPause(_menuSelect.activeSelf);
        }

        private void CloseMenu(InputAction.CallbackContext context)
        {
            if (_menuSelect.activeSelf)
                _menuSelect.SetActive(false);
                HideInterface(true);
                SetPause(false);
        }

        private void HideInterface(bool isActive)
        {
            _inputButton.SetActive(isActive);
            _statisticMenu.SetActive(isActive);
        }

        private void SetPause(bool isActive)
        {
            if (isActive)
                Time.timeScale = 0;
            else
                Time.timeScale = 1; 
        }
    }
}