using Assets.FSM;
using UnityEngine;

namespace Assets.UI.FSM
{
    public class FsmStateOrganizer : FsmState
    {
        private Fsm _fsm;
        private FsmMenuParams _fsmMenuParams;
        private GameObject _organizerMenu;

        public FsmStateOrganizer(Fsm fsm, FsmMenuParams fsmMenuParams, GameObject organizerMenu) : base(fsm)
        {
            _fsm = fsm;
            _fsmMenuParams = fsmMenuParams;
            _organizerMenu = organizerMenu;
        }

        public override void Enter()
        {
            _fsmMenuParams.SetMenu(true); 
            _fsmMenuParams.SetMenuSwitch(true);
            _organizerMenu.SetActive(true);
            _fsm.SaveState();
        }

        public override void Exit()
        {
            _organizerMenu.SetActive(false);
        }
 
    }
}