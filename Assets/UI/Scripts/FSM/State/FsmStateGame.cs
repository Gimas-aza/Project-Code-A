using UnityEngine;
using Assets.FSM;

namespace Assets.UI.FSM
{
    public class FsmStateGame : FsmState 
    {
        private Fsm _fsm;
        private FsmMenuParams _fsmMenuParams;

        public FsmStateGame(Fsm fsm, FsmMenuParams fsmMenuParams) : base(fsm)
        {
            _fsm = fsm;
            _fsmMenuParams = fsmMenuParams;
        }

        public override void Enter()
        {
            _fsmMenuParams.SetMenu(false);
        }
    }
}
