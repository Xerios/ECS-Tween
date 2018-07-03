using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECSTween
{
    [UpdateAfter(typeof(TweenTimeUpdateGroup))]
    [UpdateInGroup(typeof(TweenEasingUpdateGroup))]
    public class TweenEasingExpOutSystem : JobComponentSystem
    {
        [BurstCompile]
        [RequireComponentTag(typeof(TweenEasingExpOut))]
        struct TweenExpInEasingJob : IJobProcessComponentData<TweenTime>
        {
            public void Execute(ref TweenTime time)
            {
                time.Value = (-Mathf.Pow(2, -10 * time.Value) + 1);
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return new TweenExpInEasingJob().Schedule(this, 128, inputDeps);
        }
    }
}