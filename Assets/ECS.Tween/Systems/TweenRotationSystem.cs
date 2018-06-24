using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Jobs;

namespace ECSTween
{
    [UpdateAfter(typeof(TweenTimeUpdateGroup))]
    [UpdateAfter(typeof(TweenEasingUpdateGroup))]
    [UpdateInGroup(typeof(TweenInterpolationGroup))]
    public class TweenRotationSystem : JobComponentSystem
    {
        [BurstCompile]
        struct TweenRotationJob : IJobProcessComponentData<TweenTime, TweenRotation, Rotation>
        {
            public void Execute([ReadOnly]ref TweenTime time, [ReadOnly]ref TweenRotation rotationInterpolate, ref Rotation rotation)
            {
                rotation = new Rotation(slerp(rotationInterpolate.From, rotationInterpolate.To, time.Value));
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return new TweenRotationJob().Schedule(this, 64, inputDeps);
        }

        // Temporary fix until they update their quaternion library ( taken from latest math library and modified few things )
        // -----------------------------------------------------------------------------
        public static quaternion slerp(quaternion q1, quaternion q2, float t)
        {
            float dt = math.dot(q1.value, q2.value);
            if (dt < 0.0f)
            {
                dt = -dt;
                q2.value = -q2.value;
            }

            if (dt < 0.9995f)
            {
                float angle = math.acos(dt);
                float s = math.rsqrt(1.0f - dt * dt);    // 1.0f / sin(angle)
                float w1 = math.sin(angle * (1.0f - t)) * s;
                float w2 = math.sin(angle * t) * s;
                return new quaternion(q1.value * w1 + q2.value * w2);
            }
            else
            {
                // if the angle is small, use linear interpolation
                return math.lerp(q1, q2, t);
            }
        }
        // -----------------------------------------------------------------------------
    }
}
