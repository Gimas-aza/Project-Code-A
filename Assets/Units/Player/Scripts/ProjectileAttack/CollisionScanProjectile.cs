using Assets.Units.Player.Interfaces;
using UnityEngine;

namespace Assets.Units.Player.ProjectileAttack
{
    public class CollisionScanProjectile : Projectile
    {
        protected override void OnTargetCollision(Collision collision, IDamageable damageable)
        {
            damageable.ApplyDamage(Damage);
        }
    }
}