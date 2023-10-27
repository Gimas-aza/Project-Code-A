using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Units.Player.Base;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private Transform _spawn;
    
    private List<AttackBehaviour> _weaponList;

    private void Awake()
    {
        _weaponList = Resources.LoadAll<AttackBehaviour>("Weapons").ToList();
    }

    public List<AttackBehaviour> GetWeapons()
    {
        List<AttackBehaviour> weapons = new();

        foreach (var weapon in _weaponList)
        {
            if (weapon.IsAccessToAttack)
            {
                var weaponInstant = Instantiate(weapon, _spawn);
                weaponInstant.gameObject.SetActive(false);
                weapons.Add(weaponInstant);
            }
        }

        return weapons;
    }
}
