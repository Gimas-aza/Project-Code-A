using System;
using Assets.Units.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Units
{
    public abstract class Unit : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _health = 100f;

        public float MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = Mathf.Clamp(value, 0f, int.MaxValue);
        }

        public float Health
        {
            get => _health;
            set => _health = Mathf.Clamp(value, 0f, MaxHealth);
        }
        public UnityAction OnDamageTaken { get; set; }

        public bool IsAlive()
        {
            return _health > 0f;
        }

        public virtual void ApplyDamage(float damage)
        {
            if (damage < 0f)
                throw new ArgumentOutOfRangeException(nameof(damage));
            
            Health -= damage;
            OnDamageTaken?.Invoke();

            if (!IsAlive())
                Destroy(gameObject);
        }
    }
}