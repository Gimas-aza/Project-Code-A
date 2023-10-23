using System;
using Assets.Units.Player.Interfaces;
using UnityEngine;

namespace Assets.Units
{
    public abstract class Unit : MonoBehaviour, IDamageable
    {
        private float _health = 100f;

        public float Health
        {
            get => _health;
            set => _health = Mathf.Clamp(value, 0f, int.MaxValue);
        }

        public bool IsAlive()
        {
            return _health > 0f;
        }

        public void ApplyDamage(float damage)
        {
            if (damage < 0f)
                throw new ArgumentOutOfRangeException(nameof(damage));
            
            Health -= damage;
        }
    }
}