using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.FSM;

namespace Assets.UI.FSM
{
    public class FsmStateGame : FsmState 
    {
        private Fsm _fsm;
        private GameObject _menuSelect;
        private GameObject _statisticMenu;
        private GameObject _inputButton;

        public FsmStateGame(Fsm fsm, FsmMenuParams fsmMenuParams) : base(fsm)
        {
            _fsm = fsm;
            _menuSelect = fsmMenuParams.MenuSelect;
            _statisticMenu = fsmMenuParams.StatisticMenu;
            _inputButton = fsmMenuParams.InputButton;
        }

        public override void Enter()
        {
            CloseMenu();
        }

        private void CloseMenu()
        {
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
