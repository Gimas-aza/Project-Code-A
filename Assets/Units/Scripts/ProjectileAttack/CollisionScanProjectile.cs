using Assets.Units.Interfaces;
using UnityEngine;

namespace Assets.Units.ProjectileAttack
{
    public class CollisionScanProjectile : Projectile
    {
        protected override void OnTargetCollision(Collision collision, IDamageable damageable)
        {
            damageable.ApplyDamage(Damage);
        }
    }
}