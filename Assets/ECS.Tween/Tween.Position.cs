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
        /// <param name="from">From position</param>
        /// <param name="to">To position</param>
        /// <param name="time">Interpolation time</param>
        /// <param name="easing">Easing type</param>
        public static void Position(GameObject go, Vector3 from, Vector3 to, float time, EasingType easing = EasingType.Linear)
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();

            var entity = BaseTween(go, time, easing);
            entityManager.AddComponentData(entity, new Position(from));
            entityManager.AddComponentData(entity, new CopyTransformToGameObject());
            entityManager.AddComponentData(entity, new TweenPosition()
            {
                From = from,
                To = to
            });
        }
    }
}
