using UnityEngine;

namespace Minimap
{
    public class MinimapMarker
    {
        private readonly SpriteRenderer _renderer;
        
        public MinimapMarker(SpriteRenderer renderer) 
        {
            _renderer = renderer;
        }
    }
}
