namespace Assets.Units.Abilities.Common
{
    public interface IGenericAbility<TConfig, TLevelData>
        where TConfig : AbilityConfig<TLevelData>
        where TLevelData : AbilityLevel { }
}
