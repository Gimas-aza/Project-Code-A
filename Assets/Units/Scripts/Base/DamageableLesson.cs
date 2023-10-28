using Assets.Units.Interfaces;
using UnityEngine;

namespace Assets.Units.Base
{
    public class DamageableLesson : MonoBehaviour, IDamageable
    {
        public void ApplyDamage(float damage)
        {
            Debug.Log($"Damage {damage} is applied!");
        }
    }
}