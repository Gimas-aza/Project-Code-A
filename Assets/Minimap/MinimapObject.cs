using UnityEngine;

namespace Minimap
{
    public class MinimapObject : MonoBehaviour, IMinimapObject
    {
        [SerializeField] private Texture2D _icon;

        public Vector2 WorldPosition => GetPosition();

        public Angle Angle => GetAngle();

        public Texture2D Icon => _icon;

        private Vector2 GetPosition()
        {
            Vector3 pos = transform.position;
            return new Vector2(pos.x, pos.y);
        }

        private Angle GetAngle() =>
            (Angle) transform.rotation.eulerAngles.y;
    }
}
