using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Units.Player.Inventory
{
    [Serializable]
    public class InventoryTableData
    {
        public string OwnerId;
        public List<Item> Items = new();
        public int Capacity; 
    }
}

