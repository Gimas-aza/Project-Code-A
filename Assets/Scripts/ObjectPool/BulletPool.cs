using System.Collections.Generic;
using System.Linq;
using Assets.Units.ProjectileAttack;
using UnityEngine;

namespace Assets.ObjectPool
{
    public class BulletPool : MonoBehaviour
    {
        private Dictionary<ProjectileAttackWeapon, List<Projectile>> _bullets = new();
        private int _currentBullet;

        public void Create(ProjectileAttackWeapon weapon, Projectile projectilePrefab, int numberBullet)
        {
            _bullets.Add(weapon, new());
            for (int i = 0; i < numberBullet; i++)
            {
                if (projectilePrefab == null) return;

                var projectile = Instantiate(projectilePrefab, transform);
                projectile.gameObject.SetActive(false);
                _bullets[weapon].Add(projectile);
            }
        }

        public Projectile GetBullet(ProjectileAttackWeapon weapon, Vector3 position, Quaternion rotation)
        {
            if (_bullets[weapon].Count() == 0)
            {
                Debug.LogError("Сначала создайте пули с помощью метода Create");
                return null;
            }
            if (_bullets[weapon].Count() == _currentBullet)
                _currentBullet = 0;

            var bullet = _bullets[weapon][_currentBullet++];
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.gameObject.SetActive(true);

            return bullet;
        }

        public void ResetBullet(ProjectileAttackWeapon weapon, Projectile projectile)
        {
            var bullet = _bullets[weapon].Where(i => i.Equals(projectile)).First();

            bullet.Reset();
        }
    }
}
