using System;
using UnityEngine;

namespace Assets.Units.Player.Inventory
{
    public class Item : MonoBehaviour, IReadOnlyItem
    {
        [SerializeField] private ItemData _data = new();

        public string ItemId => _data.ItemId;
        public string Name => _data.Name;
        public string Description => _data.Description;
        public Sprite Icon => _data.Icon;
        public TagItem Tag => _data.Tag;
        public int ItemWeight => _data.ItemWeight;
    }
}
