using Assets.Units.Abilities.Common;
using UnityEngine;

namespace Assets.Units.Abilities.Stealth
{
    [CreateAssetMenu(menuName = "Abilities/Stealth/Config", fileName = "StealthAbilityConfig", order = 0)]
    public class StealthAbilityConfig : AbilityConfig<StealthAbilityLevel>
    {
        [Header("EnergyUse")]
        [SerializeField, Min(0f)] private float _energyUseInStealth = 0.2f; 
        [SerializeField, Min(0f)] private float _energyUseInStealthMove = 2f;
        
        public float EnergyUseInStealth => _energyUseInStealth;
        public float EnergyUseInStealthMove => _energyUseInStealthMove;
    }
}
