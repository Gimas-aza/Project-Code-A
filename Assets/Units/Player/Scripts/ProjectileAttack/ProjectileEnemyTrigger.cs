using Assets.Units.Enemies;
using Assets.Units;
using UnityEngine;

namespace Assets.Units.Player.ProjectileAttack
{
    [RequireComponent(typeof(Collider))]
    public class ProjectileEnemyTrigger : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private EnemyUnitTypeFlags _enemiesToDestroy;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyUnit enemyUnit))
            {
                if (!enemyUnit.IsAlive())
                    return;

                enemyUnit.ApplyDamage(_projectile.Damage);

                if (enemyUnit.IsAlive())
                {
                    OnEnemyIsAliveAfterAttack();
                }
                else
                {
                    OnEnemyIsDeadAfterAttack(enemyUnit);
                }
            }
        }

        private void OnEnemyIsAliveAfterAttack()
        {
            _projectile.DisposeProjectile();
        }

        private void OnEnemyIsDeadAfterAttack(EnemyUnit enemyUnit)
        {
            if (_enemiesToDestroy.HasFlag((EnemyUnitTypeFlags) enemyUnit.EnemyUnitType))
            {
                Destroy(enemyUnit.gameObject);
            }
            else
            {
                _projectile.DisposeProjectile();
            }
        }
    }
}