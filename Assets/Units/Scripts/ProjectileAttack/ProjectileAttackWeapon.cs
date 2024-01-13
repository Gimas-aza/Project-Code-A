using UnityEngine;
using Assets.Units.Base;
using Assets.ObjectPool;

namespace Assets.Units.ProjectileAttack
{
    public class ProjectileAttackWeapon : AttackBehaviour
    {
        [SerializeField] private Transform _weaponMuzzle;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField, Min(0)] private int _numberProjectile;
        [SerializeField] private ForceMode _forceMode = ForceMode.Impulse;
        [SerializeField] private BulletFlightType _bulletFlightType = BulletFlightType.Straight;
        [SerializeField, Min(0f)] private float _force = 10f;
        [SerializeField] private float _cooldown;
        [SerializeField, Min(0)] private float _spreadRange;
        [SerializeField] private ParticleSystem _muzzleFlash;

        private float _time;
        private BulletPool _bulletPool;

        public BulletFlightType BulletFlightType => _bulletFlightType; 

        public void Init(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
            _bulletPool.Create(this, _projectilePrefab, _numberProjectile);
        }

        private void FixedUpdate()
        {
            _time += Time.fixedDeltaTime;
        }

        [ContextMenu(nameof(PerformAttack))]
        public override void PerformAttack(float force = 0)
        {
            if (_time > _cooldown)
            {
                var angle = _weaponMuzzle.eulerAngles;
                angle.x = Random.Range(-_spreadRange, _spreadRange);
                angle.y = Random.Range(-_spreadRange, _spreadRange);

                var projectile = _bulletPool.GetBullet(this, _weaponMuzzle.position, Quaternion.Euler(angle));
                if (projectile == null) return;
                _muzzleFlash.Play();

                _weaponMuzzle.localRotation = Quaternion.Euler(angle);
                if (_bulletFlightType == BulletFlightType.Straight)
                    force = _force;

                projectile.Rigidbody.AddForce(_weaponMuzzle.forward * force, _forceMode);
                _time = 0;
            }
        }
    }
}