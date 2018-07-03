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
        /// Tween from one position to another
        /// </summary>
        /// <param name="go">GameObject</param>
        /// <param name="from">From rotation</param>
        /// <param name="to">To rotation</param>
        /// <param name="time">Interpolation time</param>
        /// <param name="easing">Easing type</param>
        public static void Rotation(GameObject go, Quaternion from, Quaternion to, float time, EasingType easing = EasingType.Linear)
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();

            var entity = BaseTween(go, time, easing);
            entityManager.AddComponentData(entity, new Rotation(from));
            entityManager.AddComponentData(entity, new TweenRotation()
            {
                From = from,
                To = to
            });
        }
    }
}
