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
            _playerSkills.StealthDamage = value;
            return this;
        }

        public SkillsBuilder SetCloaking(int value)
        {
            _playerSkills.Cloaking = value;
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