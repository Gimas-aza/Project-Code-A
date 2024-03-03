using UnityEngine;

namespace Minimap
{
    public class MinimapCenter : MonoBehaviour
    {
        [SerializeField] private float _extent;
        
        public float Extent => _extent;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected() =>
            Gizmos.DrawWireCube(transform.position, new Vector3(Extent * 2, 0, Extent * 2));
#endif
    }
}
