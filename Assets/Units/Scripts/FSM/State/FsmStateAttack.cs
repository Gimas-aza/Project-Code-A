using System;
using Assets.Units.Base;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Units.FSM
{
    public class FsmStateAttack : FsmState
    {
        private Fsm _fsm;
        private PlayerUnit _player;
        private NavMeshAgent _navMeshAgent;
        private float _pursueDistance;
        private AttackBehaviour _attackBehaviour;
        private float _cooldown;
        private float _time;
        private Light _fieldOFView;

        public FsmStateAttack(Fsm fsm, PlayerUnit player,
                AttackBehaviour attackBehaviour, float cooldown,
                NavMeshAgent navMeshAgent, float pursueDistance, 
                Light fieldOFView) : base(fsm)
        {
            _fsm = fsm;
            _player = player;
            _attackBehaviour = attackBehaviour;
            _cooldown = cooldown;
            _navMeshAgent = navMeshAgent;
            _pursueDistance = pursueDistance;
            _fieldOFView = fieldOFView;
        }

        public override void Enter()
        {
            _time = 0;
            _navMeshAgent.stoppingDistance = 4.5f;
            _fieldOFView.enabled = false;
        }

        public override void Exit()
        {
            _navMeshAgent.stoppingDistance = 0;
        }

        public override void Update()
        {
            if (Vector3.Distance(_player.transform.position, _navMeshAgent.transform.position) > _pursueDistance)
            {
                _fsm.SetState<FsmStateWalk>();
            }

            Move();
            Attack();
        }

        private void Move()
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }

        private void Attack()
        {            
            _time += Time.deltaTime;
            if (_cooldown <= _time)
            {
                _attackBehaviour.PerformAttack();
                _time = 0;
            }
        }
    }
}
