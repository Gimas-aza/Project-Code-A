using UnityEngine;

namespace Assets.Units.Abilities.Common
{
    public abstract class AbilityConfig<TLevelData> : ScriptableObject where TLevelData : AbilityLevel
    {
        // other related common properties
        [field: SerializeField] public TLevelData[] Levels { get; private set; }
    }
}
