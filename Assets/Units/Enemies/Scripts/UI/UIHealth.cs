using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Units.Enemies
{
    public class UIHealth : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }

        public void SetActive(bool isActive) 
        {
            gameObject.SetActive(isActive);
        }

        public void SetHealth(float health, float maxHealth) =>
            _healthBar.fillAmount = health / maxHealth;
    }
}