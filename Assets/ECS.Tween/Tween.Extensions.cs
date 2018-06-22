using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECSTween
{

    public static partial class Tween
    {
        /// <summary>
        /// Tween current position by a value
        /// </summary>
        /// <param name="go">GameObject</param>
        /// <param name="move">Translate position by this value</param>
        /// <param name="time">Interpolation time</param>
        /// <param name="easing">Easing type</param>
        public static void MovePosition(GameObject go, Vector3 move, float time, EasingType easing = EasingType.Linear)
        {
            var from = go.transform.position;
            Position(go, from, from + move, time, easing);
        }

        /// <summary>
        /// Tween current position to another
        /// </summary>
        /// <param name="go">GameObject</param>
        /// <param name="to">Translate position to this destination</param>
        /// <param name="time">Interpolation time</param>
        /// <param name="easing">Easing type</param>
        public static void Position(GameObject go, Vector3 to, float time, EasingType easing = EasingType.Linear)
        {
            var from = go.transform.position;
            Position(go, from, to, time, easing);
        }
    }
}
