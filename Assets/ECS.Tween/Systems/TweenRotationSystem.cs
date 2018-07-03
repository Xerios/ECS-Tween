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
                rotation = new Rotation(math.slerp(rotationInterpolate.From, rotationInterpolate.To, time.Value));
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return new TweenRotationJob().Schedule(this, 64, inputDeps);
        }
    }
}
