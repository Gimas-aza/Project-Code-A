using UnityEngine;
using Assets.Units.Interfaces;
using Assets.Units.Player;

namespace Assets.Units.ProjectileAttack
{
    [RequireComponent(typeof(OverlapProjectile))]
    public class DistractionProjectile : Projectile
    {
        private OverlapProjectile _overlap; 

        private void Start()
        {
            _overlap = GetComponent<OverlapProjectile>(); 
        }

        protected override void OnTargetCollision(Collision collision, IDamageable damageable)
        {
            damageable.ApplyDamage(Damage);
        }

        protected override void OnOtherCollision(Collision collision)
        {
            ResetRigidbody();

            _overlap.StartAction();
        }
    }
}
