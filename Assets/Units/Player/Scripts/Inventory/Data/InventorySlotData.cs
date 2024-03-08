using System;

namespace Assets.Units.Player.Inventory
{
    [Serializable]
    public class InventorySlotData
    {
        public string ItemId;
        public int Amount;
        public Item item;
    }
}
