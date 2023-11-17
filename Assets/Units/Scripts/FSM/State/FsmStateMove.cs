using UnityEngine;
using UnityEngine.AI;
using Assets.Units;

namespace Assets.Units.FSM
{
    public class FsmStateMove : FsmState
    {
        private Fsm _fsm;
        private PlayerUnit _player;
        private NavMeshAgent _navMeshAgent;
        private float _pursueDistance;
        private float _attackDistance;
        private Transform _pointAttack;

        public FsmStateMove(Fsm fsm, PlayerUnit player, NavMeshAgent navMeshAgent, 
                            float pursueDistance, float attackDistance, Transform pointAttack) : base(fsm)
        {
            _fsm = fsm;
            _player = player;
            _navMeshAgent = navMeshAgent;
            _pursueDistance = pursueDistance;
            _attackDistance = attackDistance;
            _pointAttack = pointAttack;
        } 

        public override void Enter()
        {
            _navMeshAgent.isStopped = false;
        }

        public override void Exit()
        {
            _navMeshAgent.isStopped = true;
        }

        public override void Update()
        {
            _navMeshAgent.SetDestination(_player.transform.position);
            

            if (Vector3.Distance(_player.transform.position, _pointAttack.position) <= _attackDistance)
            {
                _fsm.SetState<FsmStateAttack>();
            }
            else if (Vector3.Distance(_player.transform.position, _navMeshAgent.transform.position) > _pursueDistance)
            {
                _fsm.SetState<FsmStateIdle>();
            }
        }
    }
}