using UnityEngine;
using Zenject;
using Assets.UI;
using Assets.Units.Player;
using UnityEngine.Events;
using System.Collections;
using Assets.Units.Abilities.Stealth;

namespace Assets.Units
{
    [RequireComponent(typeof(StealthAbilityDummy))]
    public class PlayerUnit : Unit
    {
        [SerializeField, Min(0f)] private float _maxActionPoints = 100f;
        [SerializeField, Min(0f)] private float _actionPoints = 100f;
        [Header("Shield")]
        [SerializeField, Min(0f)] private float _energyUseInShield = 1f; 
        [SerializeField, Range(0f, 1f)] private float _shieldDamageAbsorption = 0.15f;

        private VitalityMonitor _vitalityMonitor;
        private PlayerMove _playerMove;
        private PlayerShoot _playerShoot;
        private Coroutine _actionPointsCoroutine;
        private IStealthAbility _energyAbility;

        public float MaxActionPoints
        {
            get => _maxActionPoints;
            set => _maxActionPoints = Mathf.Clamp(value, 0f, int.MaxValue);
        }
        public float ActionPoints
        {
            get => _actionPoints;
            set => _actionPoints = Mathf.Clamp(value, 0f, MaxActionPoints);
        }
        public bool IsStealth { get; private set; } = false;
        public bool IsShield { get; private set; } = false;

        public UnityAction<bool> OnActiveStealth;

        [Inject]
        private void Constructor(VitalityMonitor vitalityMonitor)
        {
            _vitalityMonitor = vitalityMonitor;
        }

        private void Start()
        {
            _playerMove ??= GetComponent<PlayerMove>();
            _playerShoot ??= GetComponent<PlayerShoot>();
            _vitalityMonitor.ChangeHealth((int) Health, MaxHealth);
            _vitalityMonitor.ChangeActionPoints((int) ActionPoints, MaxActionPoints);
            _energyAbility = GetComponent<IStealthAbility>();
        }

        public override void ApplyDamage(float damage)
        {
            var percent = 1f - _shieldDamageAbsorption;
            damage = IsShield ? damage * percent : damage;
            TakeOffActionPoints(Mathf.RoundToInt(damage));

            base.ApplyDamage(damage);
            _vitalityMonitor.ChangeHealth((int) Health, MaxHealth);
        }

        public void TakeOffActionPoints(int value)
        {
            if (ActionPoints == 0)
                return;
            
            ActionPoints -= value;
            _vitalityMonitor.ChangeActionPoints((int) ActionPoints, MaxActionPoints);
        }

        public void AddActionPoints(int value)
        {
            ActionPoints += value;
            _vitalityMonitor.ChangeActionPoints((int) ActionPoints, MaxActionPoints);
        }

        public void ActivateStealth()
        {
            if (!IsStealth && ActionPoints == 0 || IsShield)
                return;
            
            IsStealth = !IsStealth;
            _playerMove?.LesserSpeed(IsStealth);

            if (IsStealth)
            {
                if (_actionPointsCoroutine != null)
                    StopCoroutine(_actionPointsCoroutine);

                _vitalityMonitor.PrintConsoleText("Переход в режим крысы!! Макчимум урона!");
                _actionPointsCoroutine = StartCoroutine(TakeOffActionPointsWithinTime(1, 0.2f));
            }
            else
            {
                _vitalityMonitor.PrintConsoleText("Выход из режима крысы!!");
                StopCoroutine(_actionPointsCoroutine);
                _actionPointsCoroutine = StartCoroutine(AddActionPointsWithinTime(1, 0.1f));
            }

            OnActiveStealth?.Invoke(IsStealth);
        }

        public void ActiveShield()
        {
            if (!IsShield && ActionPoints == 0 || IsStealth)
                return;
            
            IsShield = !IsShield;
            _playerMove?.LesserSpeed(IsShield);

            if (IsShield)
            {
                if (_actionPointsCoroutine != null)
                    StopCoroutine(_actionPointsCoroutine);

                _vitalityMonitor.PrintConsoleText("Максимум брони!!");
                _actionPointsCoroutine = StartCoroutine(TakeOffActionPointsWithinTime(1, 0.1f, true));
            }
            else
            {
                _vitalityMonitor.PrintConsoleText("Минимум брони!!");
                StopCoroutine(_actionPointsCoroutine);
                _actionPointsCoroutine = StartCoroutine(AddActionPointsWithinTime(1, 0.1f));
            }
        }

        private IEnumerator TakeOffActionPointsWithinTime(int value, float time, bool isAlternate = false)
        {
            float deltaTime = 0; 
            while (ActionPoints > 0)
            {
                float multiplierTime = !isAlternate ? _energyAbility.GetEnergyConsumption() : _energyUseInShield;
                deltaTime += Time.deltaTime;
                
                if (deltaTime >= time / multiplierTime)
                {
                    deltaTime = 0;
                    TakeOffActionPoints(value);  
                }

                yield return null;
            }
            DeactivateAllAbility();
        }

        private void DeactivateAllAbility()
        {
            ActivateStealth();
            ActiveShield();
        }
        
        private IEnumerator AddActionPointsWithinTime(int value, float time)
        {
            yield return new WaitForSeconds(time * 5);

            while (ActionPoints < MaxActionPoints)
            {
                AddActionPoints(value);

                yield return new WaitForSeconds(time);
            }
        }
    }
}
