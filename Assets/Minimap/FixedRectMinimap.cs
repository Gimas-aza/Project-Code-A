using UnityEngine;

namespace Minimap
{
    public class FixedRectMinimap : MonoBehaviour
    {
        [SerializeField] private FixedRectMinimapDrawer _drawer;
        [SerializeField] private FixedRectMinimapDataBuilder _dataBuilder;

        private void Update() =>
            _drawer.Draw(_dataBuilder.BuildObjects());
    }
}
