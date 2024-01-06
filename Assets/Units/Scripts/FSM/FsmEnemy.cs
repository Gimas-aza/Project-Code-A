using Assets.FSM;
using Assets.ObjectPool;
using Assets.Units.Base;
using Assets.Units.Enemies;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Units.FSM
{
    public class FsmEnemy : MonoBehaviour
    {
        [SerializeField] private FsmEnemyParams _fsmEnemyParams = new();
        
        private Fsm _fsm;

        [Inject]
        private void Consructor(PlayerUnit player, BulletPool bulletPool)
        {
            _fsmEnemyParams.Player = player;
            _fsmEnemyParams.BulletPool = bulletPool;
        }

        private void OnValidate()
        {
            _fsmEnemyParams.NavMeshAgent ??= GetComponent<NavMeshAgent>();
            _fsmEnemyParams.AttackBehaviour ??= GetComponent<AttackBehaviour>();
            _fsmEnemyParams.OverlapAllies ??= GetComponent<OverlapAllies>();
            _fsmEnemyParams.UnitTransform ??= transform;
        }
        
        private void Start()
        {
            _fsm = new Fsm();

            _fsm.AddState(new FsmStateWalk(_fsm, _fsmEnemyParams));
            _fsm.AddState(new FsmStateAttack(_fsm, _fsmEnemyParams));

            _fsm.SetState<FsmStateWalk>(); 
        }

        private void Update()
        {
            _fsm.Update();
        }

        public void SetStateAttack()
        {
            _fsm.SetState<FsmStateAttack>();
        }
    }
}
