using System;
using Assets.Units.Player;
using UnityEngine;

namespace Assets.Units.Abilities.Stealth
{
    [RequireComponent(typeof(PlayerMove))]
    public class StealthAbilityDummy : MonoBehaviour, IStealthAbility
    {
        [SerializeField] private StealthAbilityConfig _abilityConfig;
        [SerializeField] private int _level;

        private PlayerMove _playerMove;

        private void Awake() =>
            _playerMove = GetComponent<PlayerMove>();

        private void OnValidate() =>
            _level = Math.Clamp(_level, 0, _abilityConfig?.Levels + 1 ?? 0);

        public float GetEnergyConsumption() =>
            GetPlainEnergyConsumption() * GetEnergyConsumptionModifier();

        private float GetPlainEnergyConsumption() =>
            _playerMove.IsMove ? _abilityConfig.EnergyUseInStealthMove : _abilityConfig.EnergyUseInStealth;

        private float GetEnergyConsumptionModifier() =>
            _abilityConfig.TryGetLevelData(_level - 1, out StealthAbilityLevel levelData) ?
                levelData.EnergyConsumptionModifier :
                1;
    }
}
