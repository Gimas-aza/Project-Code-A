using UnityEngine;
using Zenject;
using Assets.UI;
using Assets.Units.Player;
using UnityEngine.Events;
using System.Collections;
using System;

namespace Assets.Units
{
    public class PlayerUnit : Unit
    {
        [SerializeField] private float _maxActionPoints = 100f;
        [SerializeField] private float _actionPoints = 100f;
        [SerializeField] private PlayerSkills _playerSkills = new();

        private VitalityMonitor _vitalityMonitor;
        private PlayerMove _playerMove;
        private PlayerShoot _playerShoot;
        private Coroutine _actionPointsCoroutine;

        public SkillsBuilder Skills { get; set; }
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

        private void Awake()
        {
            Skills = new SkillsBuilder(_playerSkills);
        }

        private void Start()
        {
            _playerMove ??= GetComponent<PlayerMove>();
            _playerShoot ??= GetComponent<PlayerShoot>();
            _vitalityMonitor.ChangeHealth((int) Health, MaxHealth);
            _vitalityMonitor.ChangeActionPoints((int) ActionPoints, MaxActionPoints);
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
            float multiplierTime = 1;

            while (ActionPoints > 0)
            {
                if (_playerMove.IsMove)
                    multiplierTime = 0.5f;
                else 
                    multiplierTime = 1;
                
                TakeOffActionPoints(value);

                if (ActionPoints == 0)
                    ActivateStealth(); 
                yield return new WaitForSeconds(time * multiplierTime);
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