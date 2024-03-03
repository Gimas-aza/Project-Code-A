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

        public void Set(Vector2 position, Angle angle, Sprite icon)
        {
            _renderer.sprite = icon;

            _renderer.transform.SetLocalPositionAndRotation(
                new Vector3(position.x, position.y),
                Quaternion.Euler(0, angle, 0)
            );
        }
    }
}
