using System.Collections.Generic;
using Assets.ObjectPool;
using Assets.Units.Base;
using Assets.Units.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Units.FSM
{
    [System.Serializable]
    public class FsmEnemyParams 
    {
        [Header("Entity Control Parameters")]
        public NavMeshAgent NavMeshAgent;
        [HideInInspector] public Transform UnitTransform;
        [HideInInspector] public PlayerUnit Player;
        [Header("Attack Parameters")]
        public AttackBehaviour AttackBehaviour;
        public OverlapAllies OverlapAllies;
        [HideInInspector] public BulletPool BulletPool;
        [Header("Base Settings")]
        public List<Vector3> WaypointList;
        public List<float> WaitTimeList;
        public Light FieldOfView;
        public float ViewDistance;
        public float Fov;
        public float Cooldown;
        public float BeginDetectionTimer = 2f;
        public float MultiplierSpeed = 3f;
        public float TimeToReturnWalk = 7;
    }
}