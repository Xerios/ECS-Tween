using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECSTween
{
    // Calculates normalized time value ( 0 - 1 ) based on start time and end time
    [UpdateInGroup(typeof(TweenTimeUpdateGroup))]
    public class TweenNormalizedTimeSystem : JobComponentSystem
    {
        [BurstCompile]
        struct TweenNormalizedTimeJob : IJobProcessComponentData<TweenTime, TweenLifetime>
        {
            public float t;

            public void Execute([WriteOnly] ref TweenTime time, [ReadOnly] ref TweenLifetime range)
            {
                time.Value = (t - range.StartTime) / range.Lifetime;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return new TweenNormalizedTimeJob() { t = Time.time }.Schedule(this, 64, inputDeps);
        }
    }
}