﻿using UnityEngine;

namespace Minimap
{
    public readonly struct Angle
    {
        private readonly float _value;
        
        private Angle(float value)
        {
            _value = FilterAngle(value);
        }

        private static float FilterAngle(in float input) =>
            Mathf.Repeat(input, 360); // [0, 360) range

        public static implicit operator float(Angle angle) =>
            angle._value;

        public static explicit operator Angle(float value) =>
            new(value);
    }
}