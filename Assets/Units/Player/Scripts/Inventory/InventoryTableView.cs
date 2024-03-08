using System;
using UnityEngine;

namespace Assets.Units.Player.Inventory
{
    public class InventoryTableView : MonoBehaviour
    {
        [SerializeField] private InventoryTableData _data = new();
        [SerializeField] private Item _item;
        [SerializeField] private int _numberItem;

        private InventoryTable _inventoryTable;

        private void Awake()
        {
            _inventoryTable = new InventoryTable(_data);
        }

        [ContextMenu("Add Item")]
        private void AddItem()  
        {
            _inventoryTable.AddItems(_item, _numberItem);
            ViewItems();
        }

        [ContextMenu("Remove Item")]
        private void RemoveItem()
        {
            _inventoryTable.RemoveItems(_item.ItemId, _numberItem);
            ViewItems();
        }

        private void ViewItems()
        {
            foreach (var item in _data.Items)
                Debug.Log(item.ItemId + " " + item.Amount);
        }
    }
}
