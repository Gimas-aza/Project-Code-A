using UnityEngine;

namespace Minimap
{
    public interface IMinimapObject
    {
        Vector2 WorldPosition { get; }
        Angle Angle { get; }
        Sprite Icon { get; }
    }
}
