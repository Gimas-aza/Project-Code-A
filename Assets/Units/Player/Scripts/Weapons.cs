using System.Collections.Generic;
using System.Linq;
using Assets.ObjectPool;
using Assets.Units.Base;
using Assets.Units.ProjectileAttack;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Units.Player
{
    public class Weapons : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _numberProjectileText;

        private List<AttackBehaviour> _allWeapons;
        private List<AttackBehaviour> _allAvailableWeapons;
        private BulletPool _bulletPool;

        [Inject]
        private void Constructor(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public void Init()
        {
            _allWeapons = Resources.LoadAll<AttackBehaviour>("Weapons").ToList();
            _allAvailableWeapons = GetWeapons();
        }

        public AttackBehaviour GetWeapon(int index)
        {
            if (_allAvailableWeapons.Count == 0)
            {
                Debug.Log("Нет доступного оружия");
                return null;
            }

            foreach (var weapon in _allAvailableWeapons)
                weapon.gameObject.SetActive(false);

            _allAvailableWeapons[index].gameObject.SetActive(true);
            return _allAvailableWeapons[index];
        }

        public int GetCountWeapons() => _allAvailableWeapons.Count;

        private List<AttackBehaviour> GetWeapons()
        {
            List<AttackBehaviour> weapons = new();

            foreach (var weapon in _allWeapons)
            {
                if (weapon.IsAccessToAttack)
                {
                    var weaponInstant = Instantiate(weapon, transform);
                    SetInitForAttack(weaponInstant);
                    weaponInstant.gameObject.SetActive(false);
                    weapons.Add(weaponInstant);
                }
            }

            return weapons;
        }

        private void SetInitForAttack(AttackBehaviour weapon)
        {
            if (weapon.TryGetComponent<ProjectileAttackWeapon>(out var projectileAttackWeapon))
            {
                projectileAttackWeapon.Init(_bulletPool);
                projectileAttackWeapon.SetUI(_numberProjectileText); 
            }
        }
    }
}