using UnityEngine;

namespace Assets.Units.Abilities.Common
{
    public abstract class AbilityConfig<TLevelData> : ScriptableObject where TLevelData : AbilityLevel
    {
        // other related common properties
        [SerializeField] private TLevelData[] _levels;
        
        public int Levels => _levels.Length;

        public bool TryGetLevelData(int index, out TLevelData levelData)
        {
            if (index >= 0 && index < _levels.Length)
            {
                levelData = _levels[index];
                return true;
            }

            levelData = null;
            return false;
        }
    }
}
