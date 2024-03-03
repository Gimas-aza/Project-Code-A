using System;
using UnityEngine;

namespace Minimap
{
    public class MinimapCenter : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _extent;
        [SerializeField] private Color _gizmoColor;
        
        public float Extent => _extent;

        public Vector2 WorldPosition => new(transform.position.x, transform.position.z);

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmoColor;
            
            Vector3 pos = transform.position;
            
            var ext1 = new Vector3(_extent, 0, _extent);
            var ext2 = new Vector3(-_extent, 0, _extent);
            
            Vector3 p1 = pos - ext1;
            Vector3 p2 = pos + ext2;
            Vector3 p3 = pos + ext1;
            Vector3 p4 = pos - ext2;

            ReadOnlySpan<Vector3> points = stackalloc Vector3[] { p1, p2, p2, p3, p3, p4, p4, p1 };
            
            Gizmos.DrawLineList(points);
        }
#endif
    }
}
