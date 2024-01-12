using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Units.Player
{
    [System.Serializable]
    public class PlayerSkills
    {
        [Header("Stealth")]
        [SerializeField] private float _stealthDamage;
        [SerializeField] private float _cloaking;
        [SerializeField] private int _distraction;
        [SerializeField] private int _stealthyHacking;
        [Header("Tactic")]
        [SerializeField] private int _dataControl;
        [SerializeField] private int _tacticalVision;
        [SerializeField] private int _reconnaissance;
        [Header("Technologies")]
        [SerializeField] private int _hacking;
        [SerializeField] private int _increasedEnergy;
        [SerializeField] private int _chargingSpeed;
        [Header("Combat")]
        [SerializeField] private int _increasedHealth;
        [SerializeField] private int _energyShield;
        [SerializeField] private int _movementSpeed;

        public float StealthDamage 
        { 
            get => _stealthDamage; 
            set => _stealthDamage = Mathf.Clamp(value, 0, 5);
        }
        public float Cloaking
        {
            get => _cloaking;
            set => _cloaking = Mathf.Clamp(value, 0, 1);
        }
        public int Distraction
        {
            get => _distraction;
            set => _distraction = Mathf.Clamp(value, 0, 2);
        }
        public int StealthyHacking
        {
            get => _stealthyHacking;
            set => _stealthyHacking = Mathf.Clamp(value, 0, 3);
        }
        public int DataControl
        {
            get => _dataControl;
            set => _dataControl = Mathf.Clamp(value, 0, 2);
        }
        public int TacticalVision
        {
            get => _tacticalVision;
            set => _tacticalVision = Mathf.Clamp(value, 0, 3);
        }
        public int Reconnaissance
        {
            get => _reconnaissance;
            set => _reconnaissance = Mathf.Clamp(value, 0, 2);
        }
        public int Hacking
        {
            get => _hacking;
            set => _hacking = Mathf.Clamp(value, 0, 3);
        }
        public int IncreasedEnergy
        {
            get => _increasedEnergy;
            set => _increasedEnergy = Mathf.Clamp(value, 0, 3);
        }
        public int ChargingSpeed
        {
            get => _chargingSpeed;
            set => _chargingSpeed = Mathf.Clamp(value, 0, 3);
        }
        public int IncreasedHealth
        {
            get => _increasedHealth;
            set => _increasedHealth = Mathf.Clamp(value, 0, 3);
        }
        public int EnergyShield
        {
            get => _energyShield;
            set => _energyShield = Mathf.Clamp(value, 0, 3);
        }
        public int MovementSpeed
        {
            get => _movementSpeed;
            set => _movementSpeed = Mathf.Clamp(value, 0, 2);
        }
    }
}