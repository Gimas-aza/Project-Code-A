using Assets.FSM;
using UnityEngine;

namespace Assets.UI.FSM
{
    public class FsmStateInventory : FsmState
    {
        private Fsm _fsm;
        private FsmMenuParams _fsmMenuParams;
        private GameObject _inventoryMenu;

        public FsmStateInventory(Fsm fsm, FsmMenuParams fsmMenuParams, GameObject inventoryMenu) : base(fsm)
        {
            _fsm = fsm;
            _fsmMenuParams = fsmMenuParams;
            _inventoryMenu = inventoryMenu;
        }

        public override void Enter()
        {
            _fsmMenuParams.SetMenu(true); 
            _inventoryMenu.SetActive(true);
            _fsm.SaveState();
        }

        public override void Exit()
        {
            _inventoryMenu.SetActive(false);
        }
 
    }
}