using Assets.Units.Abilities.Common;

namespace Assets.Units.Abilities.Stealth
{
    public interface IStealthAbility : IGenericAbility<StealthAbilityConfig, StealthAbilityLevel>
    {
        float GetEnergyConsumption();
    }
}
