using Assets.Units.Enemies;
using Assets.Units.Interfaces;
using UnityEngine;

namespace Assets.Units.ProjectileAttack
{
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        [Header("Common")]
        [SerializeField, Min(0f)] private float _damage = 10f;
        [SerializeField] private ProjectileDisposeType _disposeType = ProjectileDisposeType.OnAnyCollision;

        [Header("Rigidbody")]
        [SerializeField] private Rigidbody _projectileRigidbody;
        [Header("Fly Effect")]
        [SerializeField] private TrailRenderer _flyEffect;

        [Header("Effect On Destroy")]
        [SerializeField] private bool _spawnEffectOnDestroy = true;
        [SerializeField] private ParticleSystem _effectOnDestroyPrefab;
        [SerializeField, Min(0f)] private float _effectOnDestroyLifetime = 2f;

        public bool IsProjectileDisposed { get; private set; }
        public float Damage => _damage;
        public ProjectileDisposeType DisposeType => _disposeType;
        public Rigidbody Rigidbody => _projectileRigidbody;

        private void OnCollisionEnter(Collision collision)
        {
            if (IsProjectileDisposed)
                return;
            
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                OnTargetCollision(collision, damageable);

                if (_disposeType == ProjectileDisposeType.OnTargetCollision)
                {
                    DisposeProjectile();
                }
            }
            else
            {
                OnOtherCollision(collision);
            }
            
            OnAnyCollision(collision);
            
            if (_disposeType == ProjectileDisposeType.OnAnyCollision)
            {
                DisposeProjectile();
            }
        }

        public void DisposeProjectile()
        {
            OnProjectileDispose();
            
            SpawnEffectOnDestroy();

            Reset();
            
            // IsProjectileDisposed = true;
        }

        private void SpawnEffectOnDestroy()
        {
            if (_spawnEffectOnDestroy == false)
                return;
            
            var effect = Instantiate(_effectOnDestroyPrefab, transform.position, _effectOnDestroyPrefab.transform.rotation);
            
            Destroy(effect.gameObject, _effectOnDestroyLifetime);
        }

        public void Reset()
        {
            // IsProjectileDisposed = false;

            gameObject.SetActive(false);
            _flyEffect.Clear();
            ResetRigidbody();
        }

        private void ResetRigidbody()
        {
            _projectileRigidbody.velocity = Vector3.zero;
            _projectileRigidbody.angularVelocity = Vector3.zero;
        }

        protected virtual void OnProjectileDispose() { }
        protected virtual void OnAnyCollision(Collision collision) { }
        protected virtual void OnOtherCollision(Collision collision) { }
        protected virtual void OnTargetCollision(Collision collision, IDamageable damageable) { }
    }
}