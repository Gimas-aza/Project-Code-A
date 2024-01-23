using Assets.Units.Base;
using Assets.ObjectPool;
using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Units.ProjectileAttack
{
    public class ProjectileAttackWeapon : AttackBehaviour
    {
        [SerializeField] private Transform _weaponMuzzle;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField, Min(0)] private int _numberProjectile;
        [SerializeField, Min(0)] private int _totalNumberProjectiles;
        [SerializeField, Min(0)] private float _cooldownReload;
        [SerializeField] private ForceMode _forceMode = ForceMode.Impulse;
        [SerializeField, Min(0)] private float _force = 10f;
        [SerializeField] private float _cooldown;
        [SerializeField, Min(0)] private float _spreadRange;
        [SerializeField] private ParticleSystem _muzzleFlash;

        private float _time;
        private int _numberSpentProjectile = 0;
        private TextMeshProUGUI _numberSpentProjectileText;
        private BulletPool _bulletPool;

        public void Init(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
            _bulletPool.Create(this, _projectilePrefab, _numberProjectile);
        }

        public override void Enable()
        {
            SetNumberProjectileInMagazine(_numberSpentProjectile);
        }

        public override void Disable()
        {
        }

        private void FixedUpdate()
        {
            _time += Time.fixedDeltaTime;
        }

        public void SetUI(TextMeshProUGUI numberSpentProjectile)
        {
            _numberSpentProjectileText = numberSpentProjectile;
            SetNumberProjectileInMagazine(_numberSpentProjectile);
        }

        [ContextMenu(nameof(PerformAttack))]
        public override void PerformAttack()
        {
            if (_time > _cooldown && _numberSpentProjectile > 0)
            {
                var angle = _weaponMuzzle.eulerAngles;
                angle.x = Random.Range(-_spreadRange, _spreadRange);
                angle.y = Random.Range(-_spreadRange, _spreadRange);

                var projectile = _bulletPool.GetBullet(this, _weaponMuzzle.position, Quaternion.Euler(angle));
                if (projectile == null) return;
                _muzzleFlash.Play();

                _weaponMuzzle.localRotation = Quaternion.Euler(angle);
                projectile.Rigidbody.AddForce(_weaponMuzzle.forward * _force, _forceMode);
                _time = 0;
                _numberSpentProjectile--;
                SetNumberProjectileInMagazine(_numberSpentProjectile);
            }
            else if (_numberSpentProjectile <= 0)
            {
                Reload();
            }
        }

        public override void Reload()
        {
            if (_totalNumberProjectiles <= 0) return;

            if (_numberSpentProjectile < _numberProjectile)
            {
                var number = _numberProjectile - _numberSpentProjectile;
                if (number < _totalNumberProjectiles)
                    _totalNumberProjectiles -= number;
                else
                {
                    number = _totalNumberProjectiles;
                    _totalNumberProjectiles = 0;
                }
                
                number += _numberSpentProjectile;
                StartCoroutine(SetReloadDelay(_cooldownReload, number));
            }
        }

        private IEnumerator SetReloadDelay(float delay, int number)
        {
            yield return new WaitForSeconds(delay);
            _numberSpentProjectile = number;    
            SetNumberProjectileInMagazine(_numberSpentProjectile);
        }

        private void SetNumberProjectileInMagazine(int number)
        {
            _numberSpentProjectileText.text = number.ToString() + "/" + _totalNumberProjectiles;
        }
    }
}