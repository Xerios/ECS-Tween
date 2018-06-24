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
    public class TweenRotationSystem : JobComponentSystem
    {
        struct TweenGroup
        {
            public ComponentDataArray<Rotation> rotations;
            [ReadOnly] public ComponentDataArray<TweenRotation> target;
            [ReadOnly] public ComponentDataArray<TweenTime> tweenTime;

            public int Length;
        }

        [Inject] private TweenGroup m_Tweens;

        [BurstCompile]
        struct RotationTweenJob : IJobParallelFor
        {
            public ComponentDataArray<Rotation> rotations;
            [ReadOnly] public ComponentDataArray<TweenRotation> target;
            [ReadOnly] public ComponentDataArray<TweenTime> tweenTime;

            public float dt;

            public void Execute(int i)
            {
                rotations[i] = new Rotation(slerp(target[i].From, target[i].To, tweenTime[i].Value));
            }
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

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new RotationTweenJob()
            {
                rotations = m_Tweens.rotations,
                target = m_Tweens.target,
                tweenTime = m_Tweens.tweenTime,
                dt = Time.deltaTime
            };

            return job.Schedule(m_Tweens.Length, 64, inputDeps);
        }
    }
}
