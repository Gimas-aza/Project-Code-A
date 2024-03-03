using UnityEngine;

namespace Minimap
{
    public class FixedRectMinimap : MonoBehaviour
    {
        [SerializeField] private FixedRectMinimapDrawer _drawer;
        [SerializeField] private FixedRectMinimapObjectsProvider _objectsProvider;

        private void Update() =>
            _drawer.Draw(_objectsProvider.GetObjects());
    }
}
