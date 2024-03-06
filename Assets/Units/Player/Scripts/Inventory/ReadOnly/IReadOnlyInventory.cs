using System;

namespace Assets.Units.Player.Inventory
{
    public interface IReadOnlyInventory
    {
        event Action<string, int> ItemsAdded;
        event Action<string, int> ItemsRemoved;

        string OwnerId { get; }
        
        int GetAmount(string itemId);
        bool Has(string itemId, int amount);
    }   
}

