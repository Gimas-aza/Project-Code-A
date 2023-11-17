using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Units.FSM
{
    public class FsmStateIdle : FsmState
    {
        private Fsm _fsm;
        private PlayerUnit _player;
        private Transform _unit;
        private float _pursueDistance;

        public FsmStateIdle(Fsm fsm, PlayerUnit player, Transform unit, float pursueDistance) : base(fsm)
        {
            _fsm = fsm;
            _player = player;
            _unit = unit;
            _pursueDistance = pursueDistance;
        }

        public override void Update()
        {
            if (Vector3.Distance(_player.transform.position, _unit.position) < _pursueDistance)
            {
                _fsm.SetState<FsmStateMoveAndAttack>();
            }
        }
    }
}
