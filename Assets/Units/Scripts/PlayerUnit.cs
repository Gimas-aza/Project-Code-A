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
        [Header("EnergyUse")]
        [SerializeField, Min(0f)] private float _energyUseInStealth = 1f; 
        [SerializeField, Min(0f)] private float _energyUseInStealthMove = 2f;

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
            if (!IsStealth && ActionPoints == 0)
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

        private IEnumerator TakeOffActionPointsWithinTime(int value, float time)
        {
            float deltaTime = 0; 
            while (ActionPoints > 0)
            {
                float multiplierTime = _energyAbility.GetEnergyConsumption();
                deltaTime += Time.deltaTime;
                
                if (deltaTime >= time / multiplierTime)
                {
                    deltaTime = 0;
                    TakeOffActionPoints(value);

                    if (ActionPoints == 0)
                        ActivateStealth(); 
                }

                yield return null;
            }
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
