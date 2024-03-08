using System;

namespace Assets.Units.Player.Inventory
{
    public interface IReadOnlyInventory
    {
        event Action<Item, int> ItemsAdded;
        event Action<Item, int> ItemsRemoved;

        string OwnerId { get; }
        
        int GetAmount(string itemId);
        bool Has(string itemId, int amount);
    }   
}

