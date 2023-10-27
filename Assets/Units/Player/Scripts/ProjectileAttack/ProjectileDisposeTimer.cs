using UnityEngine;

namespace Assets.Units.Player.ProjectileAttack
{
    [RequireComponent(typeof(Projectile))]
    public class ProjectileDisposeTimer : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _countdown = 15f;

        private Projectile _projectile;
        private float _elapsedTime;

        private void Awake()
        {
            _projectile = GetComponent<Projectile>();
        }

        private void OnDisable()
        {
            _elapsedTime = 0;
        }

        private void Update()
        {
            if (!_projectile.gameObject.activeSelf)
            {
                _elapsedTime = 0;
                return;
            }
            
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _countdown)
            {
                _projectile.DisposeProjectile();
                _elapsedTime = 0;
            }
        }
    }
}