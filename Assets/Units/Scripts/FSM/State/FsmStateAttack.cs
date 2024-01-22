using Assets.FSM;
using Assets.ObjectPool;
using Assets.Units.Base;
using Assets.Units.ProjectileAttack;
using UnityEngine;
using UnityEngine.AI;
using Assets.Units.Enemies;

namespace Assets.Units.FSM
{
    public class FsmStateAttack : FsmState
    {
        private Fsm _fsm;
        private PlayerUnit _player;
        private NavMeshAgent _navMeshAgent;
        private float _pursueDistance;
        private AttackBehaviour _attackBehaviour;
        private ProjectileAttackWeapon _projectileAttack;
        private float _cooldown;
        private float _time;
        private Light _fieldOFView;
        private BulletPool _bulletPool;
        private OverlapAllies _overlapAllies;
        private LayerMask _obstacleLayer;

        public FsmStateAttack(Fsm fsm, FsmEnemyParams fsmEnemyParams) : base(fsm)
        {
            _fsm = fsm;
            _player = fsmEnemyParams.Player;
            _navMeshAgent = fsmEnemyParams.NavMeshAgent;
            _pursueDistance = fsmEnemyParams.ViewDistance;
            _attackBehaviour = fsmEnemyParams.AttackBehaviour;
            _cooldown = fsmEnemyParams.Cooldown;
            _fieldOFView = fsmEnemyParams.FieldOfView;
            _bulletPool = fsmEnemyParams.BulletPool;
            _overlapAllies = fsmEnemyParams.OverlapAllies;
            _obstacleLayer = fsmEnemyParams.ObstacleLayer;
        }

        public override void Enter()
        {
            _time = 0;
            _navMeshAgent.stoppingDistance = 4.5f;
            _fieldOFView.enabled = false;
            _overlapAllies.StartAction();

            if (_attackBehaviour.TryGetComponent(out ProjectileAttackWeapon projectile))
            {
                projectile.Init(_bulletPool);
                _projectileAttack = projectile;
            }
        }

        public override void Exit()
        {
            _navMeshAgent.stoppingDistance = 0;
        }

        public override void Update()
        {
            var distance = Vector3.Distance(_player.transform.position, _navMeshAgent.transform.position);
            if (distance > _pursueDistance)
            {
                _fsm.SetState<FsmStateWalk>();
            }

            FollowPlayer(distance);

            if (_projectileAttack != null)
            {
                bool isObstacle = Physics.Linecast(_projectileAttack.transform.position, _player.transform.position, _obstacleLayer);
                if (isObstacle)
                    return;
            }
            Attack();
        }

        private void FollowPlayer(float distance)
        {
            if (_projectileAttack == null || distance > 20)
                _navMeshAgent.SetDestination(_player.transform.position);
            else
            {
                _navMeshAgent.SetDestination(_navMeshAgent.transform.position);
                _navMeshAgent.transform.LookAt(_player.transform);
            }
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
