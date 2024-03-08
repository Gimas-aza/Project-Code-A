using System;
using UnityEngine;

namespace Assets.Units.Player.Inventory
{
    [Serializable]
    public class ItemData 
    {
        public string ItemId;
        [Min(0)] 
        public int ItemWeight;
    }   
}

