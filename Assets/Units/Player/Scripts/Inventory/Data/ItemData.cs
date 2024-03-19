using System;
using UnityEngine;

namespace Assets.Units.Player.Inventory
{
    [Serializable]
    public class ItemData 
    {
        public string ItemId;
        public string Name;
        public string Description;
        public Sprite Icon;
        public TagItem Tag;
        [Min(0)] 
        public int ItemWeight;
    }   
}

