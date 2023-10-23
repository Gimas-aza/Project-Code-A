using Assets.Units.Enemies;
using UnityEngine;

namespace Assets.Units
{
    public class EnemyUnit : Unit
    {
        [SerializeField] private EnemyUnitType _enemyUnitType;

        public EnemyUnitType EnemyUnitType => _enemyUnitType;
    }
}