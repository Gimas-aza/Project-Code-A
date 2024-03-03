using System.Collections.Generic;
using UnityEngine;

namespace Minimap
{
    public class FixedRectMinimapDataBuilder : MonoBehaviour, IMinimapDataBuilder
    {
        [SerializeField] private List<MinimapObject> _objects;

        public IEnumerable<IMinimapObject> BuildObjects() =>
            _objects;

        public void Add(MinimapObject minimapObject)
        {
            if (Contains(minimapObject))
                return;
            
            _objects.Add(minimapObject);
        }

        public void Remove(MinimapObject minimapObject)
        {
            if (!Contains(minimapObject, out int index))
                return;
            
            _objects.RemoveAt(index);
        }

        private bool Contains(MinimapObject minimapObject) =>
            _objects.Contains(minimapObject);

        private bool Contains(MinimapObject minimapObject, out int index)
        {
            for (var i = 0; i < _objects.Count; i++)
            {
                MinimapObject listObj = _objects[i];

                if (!ReferenceEquals(listObj, minimapObject))
                    continue;

                index = i;
                return true;
            }

            index = default;
            return false;
        }
    }
}
