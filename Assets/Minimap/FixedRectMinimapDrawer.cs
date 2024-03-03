using System.Collections.Generic;
using UnityEngine;

namespace Minimap
{
    public class FixedRectMinimapDrawer : MonoBehaviour, IMinimapDrawer
    {
        [SerializeField] private MinimapCenter _center;
        
        private MinimapMarkersFactory _rendererFactory;
        private List<MinimapMarker> _markers;

        private void Awake()
        {
            _rendererFactory = new MinimapMarkersFactory(transform);
            _markers = new List<MinimapMarker>(8);
        }

        public void Draw(IEnumerable<IMinimapObject> objects)
        {
            var i = 0;

            foreach (IMinimapObject minimapData in objects)
            {
                if (!InCenterExtent(minimapData))
                    continue;
                
                MinimapMarker marker = GetMarker(i++);

                Vector2 diff = minimapData.WorldPosition - _center.WorldPosition;
                diff /= _center.Extent;

                marker.Set(diff, minimapData.Angle, minimapData.Icon);
            }
        }

        private bool InCenterExtent(in IMinimapObject minimapObject)
        {
            Vector2 diff = minimapObject.WorldPosition - _center.WorldPosition;
                
            diff.x = Mathf.Abs(diff.x);
            diff.y = Mathf.Abs(diff.y);
                
            return diff.x <= _center.Extent && diff.y <= _center.Extent;
        }

        private MinimapMarker GetMarker(int iteration)
        {
            if (iteration < _markers.Count)
                return _markers[iteration];

            MinimapMarker marker = _rendererFactory.Create();
            _markers.Add(marker);

            return marker;
        }
    }
}
