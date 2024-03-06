using System;

namespace Assets.Units.Player.Inventory
{
    public interface IReadOnlyInventoryTable : IReadOnlyInventory
    {
        event Action<int> CapacityChanged; 

        int Capacity { get; }
    }
}
