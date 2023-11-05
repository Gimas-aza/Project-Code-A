using System;
using UnityEngine;
using Zenject;
using Assets.UI;

namespace Assets.Units
{
    public class PlayerUnit : Unit
    {
        [SerializeField] private float _maxActionPoints = 100f;
        [SerializeField] private float _actionPoints = 100f;

        private VitalityMonitor _vitalityMonitor;

        public float MaxActionPoints
        {
            get => _maxActionPoints;
            set => _maxActionPoints = Mathf.Clamp(value, 0f, int.MaxValue);
        }

        public float ActionPoints
        {
            get => _actionPoints;
            set => _actionPoints = Mathf.Clamp(value, 0f, MaxActionPoints);
        }

        [Inject]
        private void Constructor(VitalityMonitor vitalityMonitor)
        {
            _vitalityMonitor = vitalityMonitor;
        }

        private void Start()
        {
            _vitalityMonitor.ChangeHealth((int) Health, MaxHealth);
            _vitalityMonitor.ChangeActionPoints((int) ActionPoints, MaxActionPoints);
        }

        public override void ApplyDamage(float damage)
        {
            base.ApplyDamage(damage);
            _vitalityMonitor.ChangeHealth((int) Health, MaxHealth);
        }
    }
}