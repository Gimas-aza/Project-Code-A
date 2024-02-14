using Assets.FSM;
using UnityEngine;

namespace Assets.UI.FSM
{
    public class FsmStateSkills : FsmState
    {
        private Fsm _fsm;
        private FsmMenuParams _fsmMenuParams;
        private GameObject _skillMenu;

        public FsmStateSkills(Fsm fsm, FsmMenuParams fsmMenuParams, GameObject skillsMenu) : base(fsm)
        {
            _fsm = fsm;
            _fsmMenuParams = fsmMenuParams;
            _skillMenu = skillsMenu;
        }

        public override void Enter()
        {
            _fsmMenuParams.SetMenu(true); 
            _fsmMenuParams.SetMenuSwitch(true);
            _skillMenu.SetActive(true);
            _fsm.SaveState();
        }

        public override void Exit()
        {
            _skillMenu.SetActive(false);
        }
 
    }
}