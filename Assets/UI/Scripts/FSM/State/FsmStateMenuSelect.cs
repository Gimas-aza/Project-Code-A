using Assets.FSM;
using UnityEngine;

namespace Assets.UI.FSM
{
    public class FsmStateMenuSelect : FsmState
    {
        private Fsm _fsm;
        private FsmMenuParams _fsmMenuParams;
        private GameObject _menuSelect;
        private GameObject _statisticMenu;
        private GameObject _inputButton;

        public FsmStateMenuSelect(Fsm fsm, FsmMenuParams fsmMenuParams) : base(fsm)
        {
            _fsm = fsm;
            _fsmMenuParams = fsmMenuParams;
            _menuSelect = fsmMenuParams.MenuSelect;
            _statisticMenu = fsmMenuParams.StatisticMenu;
            _inputButton = fsmMenuParams.InputButton;
        }

        public override void Enter()
        {
            _fsmMenuParams.SetMenu(true); 
            if (!_fsm.LoadSavedState())
                _fsm.SetState<FsmStateOrganizer>();
        }

        public override void Exit()
        {
        }
    }
}
