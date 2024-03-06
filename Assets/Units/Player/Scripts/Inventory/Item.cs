using System;
using UnityEngine;

namespace Assets.Units.Player.Inventory
{
    public class Item : MonoBehaviour, IReadOnlyItem
    {
        [SerializeField] private ItemData _data = new();

        public event Action<int> ItemAmountChanged;

        public string ItemId 
        { 
            get => _data.ItemId; 
        }
        public int Amount 
        { 
            get => _data.Amount; 
            set
            {
                if (_data.Amount != value)
                {
                    _data.Amount = value;
                    ItemAmountChanged?.Invoke(value);
                }
            }
        }
    }
}
