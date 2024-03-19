using System;
using System.Linq;

namespace Assets.Units.Player.Inventory
{
    public class InventoryTable : IReadOnlyInventoryTable
    {
        public event Action<Item, int> ItemsAdded;
        public event Action<Item, int> ItemsRemoved;
        public event Action<int> CapacityChanged;

        public string OwnerId => _data.OwnerId;
        public int Capacity 
        { 
            get => _data.Capacity;
            set
            {
                if (_data.Capacity != value || _data.MaxCapacity >= value)
                {
                    _data.Capacity = value;
                    CapacityChanged?.Invoke(value);
                }
            } 
        }
        public int MaxCapacity => _data.MaxCapacity;

        private readonly InventoryTableData _data;

        public InventoryTable(InventoryTableData data)
        {
            _data = data;
        }

        public AddItemsToInventoryTableResult AddItems(Item item, int amount)
        {
            var freeCapacity = MaxCapacity - Capacity;
            var maxNumberItems = item.ItemWeight > 0 ? (int)MathF.Floor(freeCapacity / item.ItemWeight) : amount;
            if (maxNumberItems < amount && maxNumberItems > 0)
            {
                AddItem(item, maxNumberItems);
                Capacity += item.ItemWeight * maxNumberItems;
                return new AddItemsToInventoryTableResult(OwnerId, amount, maxNumberItems);
            }
            else if (maxNumberItems == 0)
                return new AddItemsToInventoryTableResult(OwnerId, amount, 0);

            AddItem(item, amount);
            Capacity += item.ItemWeight * amount;
            return new AddItemsToInventoryTableResult(OwnerId, amount, amount);
        }

        private void AddItem(Item item, int amount)
        {
            ItemsAdded?.Invoke(item, amount);
            if (_data.Items.Count == 0 || _data.Items.All(i => i.ItemId != item.ItemId))
            {
                _data.Items.Add(new InventorySlot(item, amount));
                return;
            }
            
            _data.Items
                .Where(i => i.ItemId == item.ItemId)
                .FirstOrDefault().Amount += amount;
        }

        public RemoveItemsFromInventoryTableResult RemoveItems(string itemId, int amount)
        {
            var itemSlot = _data.Items
                .Where(i => i.ItemId == itemId)
                .FirstOrDefault();

            if (itemSlot == null) 
                return new RemoveItemsFromInventoryTableResult(OwnerId, amount, false);
            if (itemSlot.Amount - amount < 0)
                return new RemoveItemsFromInventoryTableResult(OwnerId, itemSlot.Amount - amount, false);
            
            itemSlot.Amount -= amount;
            Capacity -= itemSlot.Item.ItemWeight * amount;
            ItemsRemoved?.Invoke(itemSlot.Item, amount);

            if (itemSlot.Amount == 0)
                _data.Items.Remove(itemSlot);

            return new RemoveItemsFromInventoryTableResult(OwnerId, amount, true);
        }

        public int GetAmount(string itemId)
        {
            var itemSlot = _data.Items
                .Where(i => i.ItemId == itemId)
                .FirstOrDefault();
            return itemSlot == null ? 0 : itemSlot.Amount;
        }

        public Item GetItem(string itemId)
        {
            var itemSlot = _data.Items
                .Where(i => i.ItemId == itemId)
                .FirstOrDefault();
            return itemSlot.Item;
        }

        public bool Has(string itemId, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
