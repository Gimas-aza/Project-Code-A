using System;

namespace Assets.Units.Player.Inventory
{
    public readonly struct AddItemsToInventoryTableResult
    {
        public readonly string InventoryOwnerId;
        public readonly int ItemsToAddAmount;
        public readonly int ItemsAddedAmount;

        public int ItemsNotAddedAmount => ItemsToAddAmount - ItemsAddedAmount;

        public AddItemsToInventoryTableResult(string inventoryOwnerId, int itemsToAddAmount, int itemsAddedAmount)
        {
            InventoryOwnerId = inventoryOwnerId;
            ItemsToAddAmount = itemsToAddAmount;
            ItemsAddedAmount = itemsAddedAmount;
        }
    }
}
