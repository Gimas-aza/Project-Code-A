using System.Collections.Generic;
using Assets.Units.Base;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Units.FSM
{
    public class FsmEnemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private AttackBehaviour _attackBehaviour;
        [SerializeField] private Light _fieldOFView;
        [Header("Attack Settings")]
        [SerializeField] private Transform _pointAttack;
        [SerializeField, Min(0f)] private float _cooldown;
        [Header("Distances setting")]
        [SerializeField] private float _fov = 90f;
        [SerializeField] private float _viewDistance = 50f;
        [Header("Walk")]
        [SerializeField] private List<Vector3> _waypointList;
        [SerializeField] private List<float> _waitTimeList;
        
        private Fsm _fsm;
        private PlayerUnit _player;

        [Inject]
        private void Consructor(PlayerUnit player)
        {
            _player = player;
        }

        private void OnValidate()
        {
            _navMeshAgent ??= GetComponent<NavMeshAgent>();
            _attackBehaviour ??= GetComponent<AttackBehaviour>();
        }
        
        private void Start()
        {
            _fsm = new Fsm();

            _fsm.AddState(new FsmStateWalk(_fsm, _player, transform, 
                            _navMeshAgent, _fieldOFView, _viewDistance, _fov, 
                            _waypointList, _waitTimeList));
            _fsm.AddState(new FsmStateAttack(_fsm, _player, _attackBehaviour, 
                            _cooldown, _navMeshAgent, _viewDistance, _fieldOFView));

            _fsm.SetState<FsmStateWalk>(); 
        }

        private void Update()
        {
            _fsm.Update();
        }
    }

}
