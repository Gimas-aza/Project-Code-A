using Assets.FSM;
using UnityEngine;

namespace Assets.UI.FSM
{
    public class FsmStateHack : FsmState
    {
        private Fsm _fsm;
        private FsmMenuParams _fsmMenuParams;
        private RadioGame _hackMenu;

        public FsmStateHack(Fsm fsm, FsmMenuParams fsmMenuParams, RadioGame hackMenu) : base(fsm)
        {
            _fsm = fsm;
            _fsmMenuParams = fsmMenuParams;
            _hackMenu = hackMenu;
        }

        public override void Enter()
        {
            _fsmMenuParams.SetMenu(true); 
            _fsmMenuParams.SetMenuSwitch(false);
            _hackMenu.gameObject.SetActive(true);
            _hackMenu.StartMiniGame(Difficulty.Easy);
        }

        public override void Exit()
        {
            _hackMenu.gameObject.SetActive(false);
        }
 
    }
}