using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Units.Player.Inventory
{
    public class PopUpDropItems : MonoBehaviour 
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _amountItems;
        [SerializeField] private TextMeshProUGUI _currentAmountItems;
        [SerializeField] private Button _accept;
        [SerializeField] private Button _cancel;

        public Button Accept => _accept;
        public int CurrentDropItems => (int) _slider.value;

        private void Awake()
        {
            HidePopUp();
            _slider.onValueChanged.AddListener(UpdateCurrentAmountItems);
            _cancel.onClick.AddListener(HidePopUp);
            _accept.onClick.AddListener(HidePopUp);
        }

        public void Init(int maxAmountItems)
        {
            _amountItems.text = maxAmountItems.ToString();
            _slider.maxValue = maxAmountItems;
        }

        public void ShowPopUp()
        {
            _slider.value = 0;
            gameObject.SetActive(true);
        }

        public void HidePopUp()
        {
            gameObject.SetActive(false);
        }

        private void UpdateCurrentAmountItems(float value)
        {
            _currentAmountItems.text = value.ToString();
        }
    }
}
