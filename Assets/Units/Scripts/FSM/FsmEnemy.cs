using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Units.FSM
{
    public class FsmEnemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
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
        }
        
        private void Start()
        {
            _fsm = new Fsm();

            _fsm.AddState(new FsmStateIdle(_fsm, _player, transform, _pursueDistance));
            _fsm.AddState(new FsmStateMove(_fsm, _player, _navMeshAgent, _pursueDistance, _attackDistance));
            _fsm.AddState(new FsmStateAttack(_fsm, _player, transform, _attackDistance));

            _fsm.SetState<FsmStateIdle>(); 
        }

        private void Update()
        {
            _fsm.Update();
        }
    }

}
