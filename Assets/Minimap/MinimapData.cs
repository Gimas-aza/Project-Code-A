﻿using UnityEngine;

namespace Minimap
{
    public readonly struct MinimapData
    {
        private readonly Vector3 _positionData; // x, y - position; z - angle
        private readonly Texture2D _iconRef;

        public Texture2D Icon => _iconRef;
        public Vector2 WorldPosition => new(_positionData.x, _positionData.y);
        public Angle Angle => (Angle) _positionData.z;

        public MinimapData(Texture2D iconRef, Vector2 position, Angle angle)
        {
            _positionData = new Vector3(position.x, position.y, angle);
            _iconRef = iconRef;
        }

        public MinimapData(IMinimapObject minimapObject) : this(
            minimapObject.Icon,
            minimapObject.WorldPosition,
            minimapObject.Angle
        ) { }
    }
}
