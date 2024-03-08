using System;
using UnityEngine;

namespace Assets.Units.Player.Inventory
{
    public class Item : MonoBehaviour, IReadOnlyItem
    {
        [SerializeField] private ItemData _data = new();

        public string ItemId => _data.ItemId;
        public int ItemWeight => _data.ItemWeight;
    }
}
