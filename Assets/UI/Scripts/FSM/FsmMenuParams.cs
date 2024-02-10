using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UI.FSM
{
    [System.Serializable]
    public class FsmMenuParams
    {
        public GameObject MenuSelect;
        public GameObject InputButton;
        public GameObject StatisticMenu;

        public void SetMenu(bool isOpen)
        {
            if (MenuSelect.activeSelf == isOpen) return;

            MenuSelect.SetActive(isOpen);
            HideInterface(isOpen);
            SetPause(isOpen);
        }

        public void HideInterface(bool isHide)
        {
            InputButton.SetActive(!isHide);
            StatisticMenu.SetActive(!isHide);
        }

        public void SetPause(bool isActive)
        {
            if (isActive)
                Time.timeScale = 0;
            else
                Time.timeScale = 1; 
        }
    }
}
