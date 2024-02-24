using System;

namespace Assets.Units.Abilities.Common
{
    public interface IUpgradeableAbility
    {
        int MaxLevel { get; }
        
        int Level { get; }
        
        bool TrySetLevel(int level);
    }

    public static class UpgradeableAbilityExtensions
    {
        public static bool TryIncreaseLevel(this IUpgradeableAbility ability) =>
            ability.TrySetLevel(ability.Level + 1);
        
        public static bool TryDecreaseLevel(this IUpgradeableAbility ability) =>
            ability.TrySetLevel(ability.Level - 1);

        public static void AddLevels(this IUpgradeableAbility ability, int levels)
        {
            if (levels <= 0)
                return;

            ability.TrySetLevel(Math.Min(ability.MaxLevel, ability.Level + levels));
        }
        
        public static void RemoveLevels(this IUpgradeableAbility ability, int levels)
        {
            if (levels <= 0)
                return;

            ability.TrySetLevel(Math.Max(0, ability.Level - levels));
        }

        public static void ResetLevel(this IUpgradeableAbility ability) =>
            ability.TrySetLevel(0);

        public static void SetMaxLevel(this IUpgradeableAbility ability) =>
            ability.TrySetLevel(ability.MaxLevel);
    }
}
