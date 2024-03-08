using System;

namespace Assets.Units.Player.Inventory
{
    public class InventorySlot : IReadOnlyInventorySlot
    {
        private InventorySlotData _data = new();

        public event Action<int> ItemAmountChanged;

        public string ItemId 
        { 
            get => _data.ItemId; 
            private set => _data.ItemId = value;
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
        public Item Item 
        {
            get => _data.item;
            private set => _data.item = value;
        }

        public InventorySlot(Item item, int amount)
        {
            ItemId = item.ItemId;
            Amount = amount;
            Item = item;
        }
    }
}
