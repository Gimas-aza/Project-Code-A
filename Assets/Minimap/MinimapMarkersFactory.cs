using UnityEngine;

namespace Minimap
{
    public class MinimapMarkersFactory
    {
        private const string _OBJ_NAME = "Minimap Sprite";

        private readonly Transform _parent;
        private int _count;
        
        public MinimapMarkersFactory(Transform parent) 
        {
            _parent = parent;
        }

        public MinimapMarker Create()
        {
            var obj = new GameObject($"{_OBJ_NAME} ({(_count++).ToString()})");
            
            Transform transform = obj.transform;
            transform.SetParent(_parent, false);
            transform.forward = Vector3.down;
            
            return new MinimapMarker(obj.AddComponent<SpriteRenderer>());
        }
    }
}
