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

            Transform transform = _renderer.transform;
            
            transform.SetLocalPositionAndRotation(
                new Vector3(position.x, 0, position.y),
                Quaternion.Euler(transform.localEulerAngles.x, 0, -angle)
            );
        }

        public void Show() =>
            SetVisibility(true);

        public void Hide() =>
            SetVisibility(false);

        private void SetVisibility(bool visible) =>
            _renderer.forceRenderingOff = !visible;
    }
}
