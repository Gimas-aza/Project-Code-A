using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UI.FSM
{
    [System.Serializable]
    public class FsmMenuParams
    {
        private InputSystem _inputSystem;

        public GameObject MenuSelect;
        public GameObject MenuSwitch;
        public GameObject InputButton;
        public GameObject StatisticMenu;

        public void SetInputSystem(InputSystem inputSystem)
        {
            _inputSystem = inputSystem;
        }

        public void SetMenu(bool isOpen)
        {
            if (MenuSelect.activeSelf == isOpen) return;

            EnableMenuInput(isOpen);
            MenuSelect.SetActive(isOpen);
            SetPause(isOpen);
            HideInterface(isOpen);
        }

        public void SetMenuSwitch(bool isShow)
        {
            MenuSwitch.SetActive(isShow);
        }

        public void HideInterface(bool isHide)
        {
            InputButton.SetActive(!isHide);
            StatisticMenu.SetActive(!isHide);
        }

        public void SetPause(bool isActive)
        {
            if (isActive)
                Time.timeScale = 0;
            else
                Time.timeScale = 1; 
        }

        private void EnableMenuInput(bool active)
        {
            if (active)
            {
                _inputSystem.Player.Disable();
                _inputSystem.Menu.Enable();
            }
            else
            {
                _inputSystem.Menu.Disable();
                _inputSystem.Player.Enable();
            }
        }
    }
}
