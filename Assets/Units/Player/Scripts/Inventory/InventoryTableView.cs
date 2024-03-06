using System;
using UnityEngine;

namespace Assets.Units.Player.Inventory
{
    public class InventoryTableView : MonoBehaviour
    {
        [SerializeField] private InventoryTableData _data = new();

        private IReadOnlyInventoryTable _inventoryTable;

        private void Awake()
        {
            _inventoryTable = new InventoryTable(_data);
        }
    }
}
