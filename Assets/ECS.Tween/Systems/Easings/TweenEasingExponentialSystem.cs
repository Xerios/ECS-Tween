using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECSTween
{
    [UpdateAfter(typeof(TweenNormalizedTimeSystem))]
    public class TweenEasingExponentialSystem : JobComponentSystem
    {
        struct TweenData
        {
            public int Length;
            public ComponentDataArray<TweenTime> NormalizedTimes;
            [ReadOnly] public ComponentDataArray<TweenEasingExpIn> TweenEasingType; // Used for filtering only
        }

        [Inject] private TweenData m_tweenData;

        [BurstCompile]
        struct TweenEasingJob : IJobParallelFor
        {
            public float t;

            public ComponentDataArray<TweenTime> NormalizedTimes;

            public void Execute(int index)
            {
                var time = NormalizedTimes[index];
                time.Value = time.Value == 0f ? 0f : math.pow(1024f, time.Value - 1f);
                NormalizedTimes[index] = time;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new TweenEasingJob()
            {
                t = Time.time,
                NormalizedTimes = m_tweenData.NormalizedTimes
            };

            return job.Schedule(m_tweenData.Length, 64, inputDeps);
        }
    }
}