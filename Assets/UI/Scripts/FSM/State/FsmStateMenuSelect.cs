using System.Collections;
using System.Collections.Generic;
using Assets.FSM;
using UnityEngine;

namespace Assets.UI.FSM
{
    public class FsmStateMenuSelect : FsmState
    {
        private Fsm _fsm;
        private GameObject _menuSelect;
        private GameObject _statisticMenu;
        private GameObject _inputButton;

        public FsmStateMenuSelect(Fsm fsm, FsmMenuParams fsmMenuParams) : base(fsm)
        {
            _fsm = fsm;
            _menuSelect = fsmMenuParams.MenuSelect;
            _statisticMenu = fsmMenuParams.StatisticMenu;
            _inputButton = fsmMenuParams.InputButton;
        }

        public override void Enter()
        {
            OpenMenu(); 
        }

        public override void Exit()
        {
            OpenMenu(); 
        }

        private void OpenMenu()
        {
            _menuSelect.SetActive(!_menuSelect.activeSelf);
            HideInterface(!_menuSelect.activeSelf);
            SetPause(_menuSelect.activeSelf);
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
