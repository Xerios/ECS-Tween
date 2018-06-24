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
    public class TweenPositionSystem : JobComponentSystem
    {
        [BurstCompile]
        struct TweenPositionJob : IJobProcessComponentData<TweenTime, TweenPosition, Position>
        {
            public void Execute([ReadOnly]ref TweenTime time, [ReadOnly]ref TweenPosition positionInterpolate, ref Position position)
            {
                position = new Position(math.lerp(positionInterpolate.From, positionInterpolate.To, time.Value));
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return new TweenPositionJob().Schedule(this, 64, inputDeps);
        }
    }
}
