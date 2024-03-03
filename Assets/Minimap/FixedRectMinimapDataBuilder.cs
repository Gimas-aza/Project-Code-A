using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Minimap
{
    public class FixedRectMinimapDataBuilder : MonoBehaviour, IMinimapDataBuilder
    {
        [SerializeField] private List<MinimapObject> _objects;
        private readonly Func<MinimapObject, MinimapData> _selectionFunc = CreateMinimapData;
        
        public IEnumerable<MinimapData> BuildData() =>
            _objects.Select(_selectionFunc);

        private static MinimapData CreateMinimapData(MinimapObject minimapObject) =>
            new(minimapObject);
    }
}
