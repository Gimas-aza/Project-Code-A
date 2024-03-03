using System.Collections.Generic;
using UnityEngine;

namespace Minimap
{
    public class FixedRectMinimap : MonoBehaviour, IMinimapDrawer
    {
        [SerializeField] private MinimapCenter _center;
        
        private MinimapMarkersFactory _rendererFactory;
        private List<MinimapMarker> _markers;

        private void Awake()
        {
            _rendererFactory = new MinimapMarkersFactory(transform);
            _markers = new List<MinimapMarker>(8);
        }

        public void Draw(IEnumerable<MinimapData> data)
        {
            var i = 0;

            foreach (MinimapData minimapData in data)
            {
                MinimapMarker marker = GetMarker(i++);
            }
        }

        private MinimapMarker GetMarker(int iteration)
        {
            if (_markers.Count < iteration)
                return _markers[iteration];

            MinimapMarker marker = _rendererFactory.Create();
            _markers.Add(marker);

            return marker;
        }
    }
}
