using Assets.Units.Abilities.Common;
using UnityEngine;

namespace Assets.Units.Abilities.Stealth
{
    [CreateAssetMenu(menuName = "Abilities/Stealth/Level", fileName = "StealthAbilityLevel", order = 0)]
    public class StealthAbilityLevel : AbilityLevel
    {
        [field: SerializeField, Min(0)] public float EnergyConsumptionModifier { get; private set; } = 1f;
    }
}
