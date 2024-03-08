using System;

namespace Assets.Units.Player.Inventory
{
    public interface IReadOnlyInventorySlot
    {
        event Action<int> ItemAmountChanged;

        int Amount { get; }
    }
}
