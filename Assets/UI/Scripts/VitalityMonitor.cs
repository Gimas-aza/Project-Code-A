using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    public class VitalityMonitor : MonoBehaviour
    {
        [SerializeField, Range(0, 100)] private int _healthInt;
        [SerializeField, Range(0, 100)] private int _actionPointsInt;
        [SerializeField] private Image _health;
        [SerializeField] private TMP_Text _healthPercent;
        [SerializeField] private Image _actionPoints;
        [SerializeField] private TMP_Text _actionPointsPercent;
        [SerializeField] private TMP_Text _consoleText;

        private bool _isCritical = true;

        private void Start()
        {
            ClearConsoleText();
        }

        private void OnValidate()
        {
            ChangeHealth(_healthInt, 100f);
            ChangeActionPoints(_actionPointsInt, 100f);
        }

        public void ChangeHealth(int health, float maxHealth)
        {
            if (CheckedValues(health, maxHealth)) return;

            if (health <= 10 && _isCritical)
            {
                PrintConsoleText("Критические покозатели здоровья!");
                _isCritical = false;
            }
            else if (health >= 15)
            {
                ClearOldRecords();
                _isCritical = true;
            }

            _health.fillAmount = health / maxHealth;
            _healthPercent.text = health.ToString() + "%";
        }

        public void ChangeActionPoints(int actionPoints, float maxActionPoints)
        {
            if (CheckedValues(actionPoints, maxActionPoints)) return; 
            _actionPoints.fillAmount = actionPoints / maxActionPoints;
            _actionPointsPercent.text = actionPoints.ToString() + "%";
        }

        public void PrintConsoleText(string text)
        {
            _consoleText.text += "Log: " + text + "\n";
        }

        public void ClearConsoleText()
        {
            _consoleText.text = "";
        }

        private void ClearOldRecords(int length = 170)
        {
            if (_consoleText.text.Length < length * 2) return;
            _consoleText.text = _consoleText.text.Remove(0, length);
        }

        private bool CheckedValues(int value, float maxValue)
        {
            if (value < 0)
            {
                Debug.LogError("Value cannot be negative");
                return true;
            }
            if (value > maxValue)
            {
                Debug.LogError("Value cannot exceed max Value");
                return true;
            }

            return false;
        }
    }
}
