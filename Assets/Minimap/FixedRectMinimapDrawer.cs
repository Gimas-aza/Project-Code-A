using System.Collections.Generic;
using UnityEngine;

namespace Minimap
{
    public class FixedRectMinimapDrawer : MonoBehaviour, IMinimapDrawer
    {
        [SerializeField] private MinimapCenter _center;
        [SerializeField, Min(0)] private float _size = 1f;
        
        private MinimapMarkersFactory _rendererFactory;
        private List<MinimapMarker> _markers;
        private int _lastMarkersCount;

        private void Awake()
        {
            _rendererFactory = new MinimapMarkersFactory(transform);
            _markers = new List<MinimapMarker>(8);
        }

        public void Draw(IEnumerable<IMinimapObject> objects)
        {
            var markersDrawn = 0;

            foreach (IMinimapObject minimapObject in objects)
            {
                if (!InCenterExtent(minimapObject))
                    continue;

                DrawMarker(GetMarker(markersDrawn++), minimapObject);
            }
            
            HideTrailingMarkers(markersDrawn);
        }

        private void DrawMarker(MinimapMarker marker, IMinimapObject minimapObject)
        {
            Vector2 markerPos = CalculateMarkerNormalizedPos(minimapObject);
            markerPos *= _size;

            marker.Set(markerPos, minimapObject.Angle, minimapObject.Icon);
        }

        private void HideTrailingMarkers(int markersDrawn)
        {
            if (_lastMarkersCount > markersDrawn)
                for (int j = markersDrawn; j < _lastMarkersCount; j++)
                    _markers[j].Hide();
            
            _lastMarkersCount = markersDrawn;
        }

        private Vector2 CalculateMarkerNormalizedPos(IMinimapObject minimapData) =>
            (minimapData.WorldPosition - _center.WorldPosition) / _center.Extent;

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
            {
                MinimapMarker cachedMarker = _markers[iteration];
                cachedMarker.Show();
                
                return cachedMarker;
            }

            MinimapMarker createdMarker = _rendererFactory.Create();
            _markers.Add(createdMarker);

            return createdMarker;
        }
    }
}
