using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Units.FSM
{
    public class FsmStateWalk : FsmState
    {
        private Fsm _fsm;
        private PlayerUnit _player;
        private Transform _unit;
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
        private float _startDetectionTimer = 1f;
        private float _detectionTimer; 
        private float _beginSpeed;
        private float _multiplierSpeed = 1.3f;

        public FsmStateWalk(Fsm fsm, PlayerUnit player, Transform unit, NavMeshAgent navMeshAgent, 
                            Light fieldOfView, float viewDistance, float fov, 
                            List<Vector3> waypointList, List<float> waitTimeList) : base(fsm)
        {
            _fsm = fsm;
            _player = player;
            _unit = unit;
            _navMeshAgent = navMeshAgent;
            _fieldOFView = fieldOfView;
            _viewDistance = viewDistance;
            _fov = fov;
            _waypointList = waypointList;
            _waitTimeList = waitTimeList;
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

            _unit.GetComponent<EnemyUnit>().OnDamageTaken += MoveLastPointPlayer;
        }

        public override void Exit()
        {
            _navMeshAgent.speed = _beginSpeed;
        }

        public override void Update()
        {
            _waitTimer -= Time.deltaTime;
            if (_waitTimer <= 0f)
            {
                Movement();
                SetView(_beginFov, _viewDistance);
                _startDetectionTimer = 1f;
            }

            FindTargetPlayer();
        }

        private void Movement()
        {
            Vector3 waypoint = _waypointList[_waypointIndex];

            Vector3 waypointDir = waypoint;
            _lastMoveDir = waypointDir;

            float distanceBefore = Vector3.Distance(_unit.position, waypoint);
            _navMeshAgent.SetDestination(waypointDir);
            float distanceAfter = Vector3.Distance(_unit.position, waypoint);

            float arriveDistance = .1f;
            if (distanceAfter < arriveDistance || distanceBefore < distanceAfter) {

                _waitTimer = _waitTimeList[_waypointIndex];
                _waypointIndex = (_waypointIndex + 1) % _waypointList.Count;
            }
        }

        private void FindTargetPlayer()
        {
            Vector3 positionUnit = _unit.transform.position + _unit.transform.up * 2;
            Vector3 positionPlayer = _player.transform.position + _player.transform.up * 2;

            if (Vector3.Distance(positionUnit, positionPlayer) < _viewDistance) 
            {
                Vector3 dirToPlayer = (positionPlayer - positionUnit).normalized;
                if (Vector3.Angle(_unit.transform.forward, dirToPlayer) < _fov / 2f) 
                {
                    bool isRaycast = Physics.Raycast(positionUnit, dirToPlayer, out RaycastHit hitInfo, _viewDistance);
                    if (hitInfo.collider != null && hitInfo.collider.gameObject.TryGetComponent(out PlayerUnit player)) 
                    {
                        
                        if (player.IsStealth)
                        {
                            _detectionTimer -= Time.deltaTime;
                            Debug.Log($"Detection timer: {_detectionTimer}");
                            if (_detectionTimer <= 0f)
                            {
                                _fsm.SetState<FsmStateAttack>();
                            }
                        }
                        else
                        {
                            ResetDetectionTimer();
                            _fsm.SetState<FsmStateAttack>();
                        }
                    }
                    else
                        ResetDetectionTimer();
                }
                else if (Vector3.Distance(positionUnit, positionPlayer) < 15)
                {
                    ResetDetectionTimer();
                    _fsm.SetState<FsmStateAttack>();
                }
                else
                    ResetDetectionTimer();
            }
            else
                ResetDetectionTimer();
        }

        private void ResetDetectionTimer()
        {
            _detectionTimer = _startDetectionTimer;
        }

        private void MoveLastPointPlayer()
        {
            SetView(_beginFov * 1.5f, _viewDistance);
            _navMeshAgent.SetDestination(_player.transform.position);
            _startDetectionTimer = 0.5f;
            _waitTimer = 7;
        }

        private void SetView(float fov, float viewDistance)
        {
            _fov = fov;
            _fieldOFView.spotAngle = fov;
            _fieldOFView.range = viewDistance;
        }

        public Vector3 GetAimDir() 
        {
            return _lastMoveDir;
        }
    }
}
