using System;
using Assets.ObjectPool;
using Assets.Units.Base;
using Assets.Units.ProjectileAttack;
using Assets.Units.OverlapAttack;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
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
        private LayerMask _obstacleLayer = LayerMask.GetMask("Obstacle");

        public FsmStateAttack(Fsm fsm, PlayerUnit player,
                AttackBehaviour attackBehaviour, float cooldown,
                NavMeshAgent navMeshAgent, float pursueDistance, 
                Light fieldOFView, BulletPool bulletPool, OverlapAllies overlapAllies) : base(fsm)
        {
            _fsm = fsm;
            _player = player;
            _attackBehaviour = attackBehaviour;
            _cooldown = cooldown;
            _navMeshAgent = navMeshAgent;
            _pursueDistance = pursueDistance;
            _fieldOFView = fieldOFView;
            _bulletPool = bulletPool;
            _overlapAllies = overlapAllies;
        }

        public override void Enter()
        {
            _time = 0;
            _navMeshAgent.stoppingDistance = 4.5f;
            _fieldOFView.enabled = false;
            _overlapAllies.AttackEnemies();

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
