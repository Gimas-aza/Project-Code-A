using System;

namespace Assets.Units.Player.Inventory
{
    public class InventoryTable : IReadOnlyInventoryTable
    {
        public event Action<string, int> ItemsAdded;
        public event Action<string, int> ItemsRemoved;
        public event Action<int> CapacityChanged;

        public string OwnerId => _data.OwnerId;

        public int Capacity 
        { 
            get => _data.Capacity;
            set
            {
                if (_data.Capacity != value)
                {
                    _data.Capacity = value;
                    CapacityChanged?.Invoke(value);
                }
            } 
        }

        private readonly InventoryTableData _data;

        public InventoryTable(InventoryTableData data)
        {
            _data = data;
        }

        public int GetAmount(string itemId)
        {
            throw new NotImplementedException();
        }

        public bool Has(string itemId, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
