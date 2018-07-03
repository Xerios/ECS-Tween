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
        /// Initiates base entity to work with tweens ( internal function )
        /// </summary>
        /// <param name="go">GameObject</param>
        /// <param name="time">Interpolation time</param>
        /// <param name="easing">Easing type</param>
        private static Entity BaseTween(GameObject go, float time, EasingType easing = EasingType.Linear)
        {
            var entityManager = World.Active.GetExistingManager<EntityManager>();

            // Link a (new) entity with an existing GameObject
            var entity = GameObjectEntity.AddToEntityManager(entityManager, go);

            // Add our componentDatas
            entityManager.AddComponentData(entity, new TweenTime());
            entityManager.AddComponentData(entity, new TweenLifetime() { StartTime = Time.time, Lifetime = time });

            // Setup easing componentdata depending on easing type
            switch (easing)
            {
                case EasingType.ExpIn:
                    entityManager.AddComponentData(entity, new TweenEasingExpIn());
                    break;
                case EasingType.ExpOut:
                    entityManager.AddComponentData(entity, new TweenEasingExpOut());
                    break;
                case EasingType.Linear:
                // Nothing happens here since we don't need to modify the normalized time value
                default:
                    break;
            }

            return entity;
        }
    }
}
