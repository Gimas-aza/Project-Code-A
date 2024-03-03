using System.Collections.Generic;
using UnityEngine;

namespace Minimap
{
    public class FixedRectMinimapDataBuilder : MonoBehaviour, IMinimapDataBuilder
    {
        [SerializeField] private List<MinimapObject> _objects;

        public IEnumerable<IMinimapObject> BuildObjects() =>
            _objects;
    }
}
