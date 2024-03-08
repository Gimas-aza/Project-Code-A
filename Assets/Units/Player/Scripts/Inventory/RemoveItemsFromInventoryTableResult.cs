using System;

namespace Assets.Units.Player.Inventory
{
    public struct RemoveItemsFromInventoryTableResult
    {
        public readonly string InventoryOwnerId;
        public readonly int ItemsToRemoveAmount;
        public readonly bool Success;

        public RemoveItemsFromInventoryTableResult(string inventoryOwnerId, int itemsToRemoveAmount, bool success)
        {
            InventoryOwnerId = inventoryOwnerId;
            ItemsToRemoveAmount = itemsToRemoveAmount;
            Success = success;
        }
    }
}
