using UnityEngine;

namespace Minimap
{
    public class MinimapObject : MonoBehaviour, IMinimapObject
    {
        [SerializeField] private Sprite _icon;

        public Vector2 WorldPosition => GetPosition();

        public Angle Angle => GetAngle();

        public Sprite Icon => _icon;

        private Vector2 GetPosition()
        {
            Vector3 pos = transform.position;
            return new Vector2(pos.x, pos.z);
        }

        private Angle GetAngle() =>
            (Angle) transform.eulerAngles.y;
    }
}
