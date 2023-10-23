using System;

namespace Assets.Units.Enemies
{
    public enum EnemyUnitType
    {
        Small = EnemyUnitTypeFlags.Small,
        Medium = EnemyUnitTypeFlags.Medium,
        Big = EnemyUnitTypeFlags.Big,
        Boss = EnemyUnitTypeFlags.Boss
    }

    [Flags]
    public enum EnemyUnitTypeFlags
    {
        None = 0,
        Small = 1,
        Medium = 2,
        Big = 4,
        Boss = 8
    }
}