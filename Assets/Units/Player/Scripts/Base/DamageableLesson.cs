using Assets.Units.Player.Interfaces;
using UnityEngine;

namespace Assets.Units.Player.Base
{
    public class DamageableLesson : MonoBehaviour, IDamageable
    {
        public void ApplyDamage(float damage)
        {
            Debug.Log($"Damage {damage} is applied!");
        }
    }
}