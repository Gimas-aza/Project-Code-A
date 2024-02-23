namespace Assets.Units.Abilities.Common
{
    // todo: rework to interface
    public abstract class AbstractAbility<TConfig, TLevelData>
        where TConfig : AbilityConfig<TLevelData>
        where TLevelData : AbilityLevel { }
}
