using System.Collections;
using System.Collections.Generic;
using Assets.Units;
using UnityEngine;

namespace Assets.Units.Player
{
    public class SkillsBuilder
    {
        private PlayerSkills _playerSkills;

        public SkillsBuilder(PlayerSkills playerSkills) 
        {
            _playerSkills = playerSkills;
        } 
        
        public SkillsBuilder SetStealthDamage(int value)
        {
            switch (value)
            {
                case 0:
                    _playerSkills.StealthDamage = 2;
                    break;
                case 1:
                    _playerSkills.StealthDamage = 2.5f;
                    break;
                case 2:
                    _playerSkills.StealthDamage = 3;
                    break;
                case 3:
                    _playerSkills.StealthDamage = 3.5f;
                    break;
            }
            return this;
        }

        public SkillsBuilder SetCloaking(int value)
        {
            switch (value)
            {
                case 0:
                    _playerSkills.Cloaking = 0;
                    break;
                case 1:
                    _playerSkills.Cloaking = 0.2f;
                    break;
                case 2:
                    _playerSkills.Cloaking = 0.4f;
                    break;
                case 3:
                    _playerSkills.Cloaking = 0.6f;
                    break;
            }
            return this;
        }

        public SkillsBuilder SetDistraction(int value)
        {
            _playerSkills.Distraction = value;
            return this;
        }

        public SkillsBuilder SetStealthyHacking(int value)
        {
            _playerSkills.StealthyHacking = value;
            return this;
        }

        public SkillsBuilder SetDataControl(int value)
        {
            _playerSkills.DataControl = value;
            return this;
        }

        public SkillsBuilder SetTacticalVision(int value)
        {
            _playerSkills.TacticalVision = value;
            return this;
        }

        public SkillsBuilder SetReconnaissance(int value)
        {
            _playerSkills.Reconnaissance = value;
            return this;
        }

        public SkillsBuilder SetHacking(int value)
        {
            _playerSkills.Hacking = value;
            return this;
        }

        public SkillsBuilder SetIncreasedEnergy(int value)
        {
            _playerSkills.IncreasedEnergy = value;
            return this;
        }

        public SkillsBuilder SetChargingSpeed(int value)
        {
            _playerSkills.ChargingSpeed = value;
            return this;
        }

        public SkillsBuilder SetIncreasedHealth(int value)
        {
            _playerSkills.IncreasedHealth = value;
            return this;
        }

        public SkillsBuilder SetEnergyShield(int value)
        {
            _playerSkills.EnergyShield = value;
            return this;
        }

        public SkillsBuilder SetMovementSpeed(int value)
        {
            _playerSkills.MovementSpeed = value;
            return this;   
        }

        public PlayerSkills Build() => _playerSkills;
    }
}