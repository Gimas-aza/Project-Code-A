using Assets.Units.Enemies;
using UnityEngine;

namespace Assets.Units
{
    public class EnemyUnit : Unit
    {
        [Header("UI")]
        [SerializeField] private bool _isActiveHealthBar = true;
        [SerializeField] private UIHealth _uiHealthBar;
        [SerializeField] private bool _isActiveFieldOfView = true;
        [SerializeField] private Light _fieldOfView;
        [Header("Settings")]
        [SerializeField] private EnemyUnitType _enemyUnitType;

        public EnemyUnitType EnemyUnitType => _enemyUnitType;

        private void Start()
        {
            _uiHealthBar.SetActive(_isActiveHealthBar);
            _fieldOfView.enabled = _isActiveFieldOfView; 
        }

        public override void ApplyDamage(float damage)
        {
            base.ApplyDamage(damage);
            _uiHealthBar.SetHealth(Health, MaxHealth);
        }

        public void SetActiveHealthBar(bool isActive) =>
            _uiHealthBar.SetActive(isActive);        
        
        public void SetActiveLight(bool isActive) =>
            _fieldOfView.enabled = isActive;
    }
}