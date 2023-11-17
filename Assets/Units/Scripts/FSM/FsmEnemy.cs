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
        [Header("Attack Settings")]
        [SerializeField] private Transform _pointAttack;
        [SerializeField, Min(0f)] private float _cooldown;
        [Header("Distances setting")]
        [SerializeField] private float _pursueDistance;
        [SerializeField] private float _attackDistance;
        
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

            _fsm.AddState(new FsmStateIdle(_fsm, _player, transform, _pursueDistance));
            _fsm.AddState(new FsmStateMoveAndAttack(_fsm, _player, _attackBehaviour, 
                            _cooldown, _navMeshAgent, _pursueDistance));

            _fsm.SetState<FsmStateIdle>(); 
        }

        private void Update()
        {
            _fsm.Update();
        }
    }

}
