using System;

namespace Assets.Units.Player.Inventory
{
    public interface IReadOnlyItem
    {
        event Action<int> ItemAmountChanged; 

        string ItemId { get; }
        int Amount { get; }
    }
}
