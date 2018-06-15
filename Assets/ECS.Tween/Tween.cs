using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECSTween
{
    public enum EasingType
    {
        Linear,
        ExpIn
    }

    public static class Tween
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
            var entityManager = World.Active.GetExistingManager<EntityManager>();
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
            var entityManager = World.Active.GetExistingManager<EntityManager>();
            var from = go.transform.position;
            Position(go, from, to, time, easing);
        }

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

            var pos = go.transform.position;

            var entity = GameObjectEntity.AddToEntityManager(entityManager, go);
            entityManager.AddComponentData(entity, new Position() { Value = pos });
            entityManager.AddComponentData(entity, new CopyTransformToGameObject());
            entityManager.AddComponentData(entity, new TweenTime());
            entityManager.AddComponentData(entity, new TweenLifetime() { StartTime = Time.time, Lifetime = time });
            entityManager.AddComponentData(entity, new TweenTarget()
            {
                From = pos,
                To = to
            });

            switch (easing)
            {
                case EasingType.ExpIn:
                    entityManager.AddComponentData(entity, new TweenEasingExpIn());
                    break;
                case EasingType.Linear:
                default:
                    break;
            }
        }
    }
}
