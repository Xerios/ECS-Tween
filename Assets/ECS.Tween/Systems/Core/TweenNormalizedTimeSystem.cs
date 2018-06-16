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
        struct TweenData
        {
            public int Length;
            public ComponentDataArray<TweenTime> Times;
            [ReadOnly] public ComponentDataArray<TweenLifetime> TimeRanges;
        }

        [Inject] private TweenData m_tweenData;

        [BurstCompile]
        struct TweenTimeJob : IJobParallelFor
        {
            public float t;

            public ComponentDataArray<TweenTime> Times;
            [ReadOnly] public ComponentDataArray<TweenLifetime> TimeRanges;

            public void Execute(int index)
            {
                var range = TimeRanges[index];
                Times[index] = new TweenTime()
                {
                    Value = (t - range.StartTime) / range.Lifetime
                };
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new TweenTimeJob()
            {
                t = Time.time,
                Times = m_tweenData.Times,
                TimeRanges = m_tweenData.TimeRanges
            };

            return job.Schedule(m_tweenData.Length, 64, inputDeps);
        }
    }
}