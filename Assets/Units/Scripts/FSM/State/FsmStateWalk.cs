using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Units.FSM
{
    public class FsmStateWalk : FsmState
    {
        private Fsm _fsm;
        private PlayerUnit _player;
        private Transform _unitTransform;
        private EnemyUnit _unitEnemy;
        private NavMeshAgent _navMeshAgent;
        private Light _fieldOFView;
        private float _viewDistance;
        private float _beginFov;
        private float _fov;
        private List<Vector3> _waypointList;
        private List<float> _waitTimeList;
        private Vector3 _lastMoveDir;
        private int _waypointIndex;
        private float _waitTimer;
        private float _beginDetectionTimer;
        private float _detectionTimer; 
        private float _beginSpeed;
        private float _multiplierSpeed;
        private float _timeToReturnWalk;

        public FsmStateWalk(Fsm fsm, FsmEnemyParams fsmEnemyParams) : base(fsm)
        {
            _fsm = fsm;
            _player = fsmEnemyParams.Player;
            _unitTransform = fsmEnemyParams.UnitTransform;
            _navMeshAgent = fsmEnemyParams.NavMeshAgent;
            _fieldOFView = fsmEnemyParams.FieldOfView;
            _viewDistance = fsmEnemyParams.ViewDistance;
            _fov = fsmEnemyParams.Fov;
            _waypointList = fsmEnemyParams.WaypointList;
            _waitTimeList = fsmEnemyParams.WaitTimeList;
            _beginDetectionTimer = fsmEnemyParams.BeginDetectionTimer;
            _multiplierSpeed = fsmEnemyParams.MultiplierSpeed;
            _timeToReturnWalk = fsmEnemyParams.TimeToReturnWalk;
        }

        public override void Enter()
        {
            _beginSpeed = _navMeshAgent.speed;
            _navMeshAgent.speed = _beginSpeed / _multiplierSpeed;

            _waypointIndex = 0;
            _waitTimer = _waitTimeList[0];
            ResetDetectionTimer();

            _beginFov = _fov;
            SetView(_beginFov, _viewDistance);
            _fieldOFView.enabled = true;

            _unitEnemy = _unitTransform.GetComponent<EnemyUnit>();
            _unitEnemy.OnDamageTaken += TryMoveLastPointPlayer;
            _player.OnActiveStealth += (bool value) => _unitEnemy.IsIncreaseDamage = value;
        }

        public override void Exit()
        {
            _unitEnemy.OnDamageTaken -= TryMoveLastPointPlayer;
            _player.OnActiveStealth -= (bool value) => _unitEnemy.IsIncreaseDamage = value;

            _navMeshAgent.speed = _beginSpeed;
            _unitEnemy.IsIncreaseDamage = false;

            ResetDetectionTimer();
        }

        public override void Update()
        {
            if (_navMeshAgent.remainingDistance.CompareTo(0f) == 0)
                _waitTimer -= Time.deltaTime;
            if (_waitTimer <= 0f)
            {
                Movement();
                SetView(_beginFov, _viewDistance);
                _beginDetectionTimer = 2f;
            }

            FindTargetPlayer();
        }

        private void Movement()
        {
            Vector3 waypoint = _waypointList[_waypointIndex];

            Vector3 waypointDir = waypoint;
            _lastMoveDir = waypointDir;

            _navMeshAgent.SetDestination(waypointDir);
            float distanceAfter = Vector3.Distance(_unitTransform.position, waypoint);

            float arriveDistance = .1f;
            if (distanceAfter > arriveDistance) {

                _waitTimer = _waitTimeList[_waypointIndex];
                _waypointIndex = (_waypointIndex + 1) % _waypointList.Count;
            }
        }

        private void FindTargetPlayer()
        {
            Vector3 positionUnit = _unitTransform.transform.position + _unitTransform.transform.up * 2;
            Vector3 positionPlayer = _player.transform.position + _player.transform.up * 2;

            if (Vector3.Distance(positionUnit, positionPlayer) < _viewDistance) 
            {
                Vector3 dirToPlayer = (positionPlayer - positionUnit).normalized;
                if (Vector3.Angle(_unitTransform.transform.forward, dirToPlayer) < _fov / 2f) 
                {
                    bool isRaycast = Physics.Raycast(positionUnit, dirToPlayer, out RaycastHit hitInfo, _viewDistance);
                    if (hitInfo.collider != null && hitInfo.collider.gameObject.TryGetComponent(out PlayerUnit player)) 
                    {
                        
                        if (player.IsStealth)
                        {
                            _detectionTimer -= Time.deltaTime;
                            if (_detectionTimer <= 0f)
                                _fsm.SetState<FsmStateAttack>();

                            return;
                        }
                        else
                        {
                            _fsm.SetState<FsmStateAttack>();
                        }
                    }
                }
                else if (Vector3.Distance(positionUnit, positionPlayer) < 15)
                    _fsm.SetState<FsmStateAttack>();
            }
            
            ResetDetectionTimer();
        }

        private void ResetDetectionTimer()
        {
            _detectionTimer = _beginDetectionTimer;
        }

        private async void TryMoveLastPointPlayer()
        {
            if (!_player.IsStealth)
            {
                _fsm.SetState<FsmStateAttack>();
                return;
            }

            SetView(_beginFov * 1.5f, _viewDistance);
            _beginDetectionTimer = 1f;
            _waitTimer = _timeToReturnWalk;

            await SetDestinationWithDelay(_player.transform.position, 1000);
        }

        private void SetView(float fov, float viewDistance)
        {
            _fov = fov;
            _fieldOFView.spotAngle = fov;
            _fieldOFView.range = viewDistance;
        }

        private async Task SetDestinationWithDelay(Vector3 waypoint, int delay)
        {
            await Task.Delay(delay);
            _navMeshAgent.SetDestination(waypoint);
        }

        public Vector3 GetAimDir() 
        {
            return _lastMoveDir;
        }
    }
}
