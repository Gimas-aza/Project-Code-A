using UnityEngine;
using Assets.Units;
using Assets.Units.Base;

namespace Assets.Units.FSM
{
    public class FsmStateAttack : FsmState
    {
        private Fsm _fsm;
        private PlayerUnit _player;
        private float _attackDistance;
        private AttackBehaviour _attackBehaviour;
        private Transform _pointAttack;
        private float _cooldown;
        private float _time;

        public FsmStateAttack(Fsm fsm, PlayerUnit player,
                float attackDistance, AttackBehaviour attackBehaviour, float cooldown,
                Transform pointAttack) : base(fsm)
        {
            _fsm = fsm;
            _player = player;
            _attackDistance = attackDistance;
            _attackBehaviour = attackBehaviour;
            _pointAttack = pointAttack;
            _cooldown = cooldown;
        }

        public override void Enter()
        {
            _time = 0;
        }

        public override void Update()
        {
            if (Vector3.Distance(_player.transform.position, _pointAttack.position) > _attackDistance)
            {
                _fsm.SetState<FsmStateMove>();
                return;
            }
            
            _time += Time.deltaTime;
            if (_cooldown <= _time)
            {
                _attackBehaviour.PerformAttack();
                _time = 0;
            }
        }
    }
}