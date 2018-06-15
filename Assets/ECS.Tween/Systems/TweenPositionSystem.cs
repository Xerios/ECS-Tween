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
    [UpdateAfter(typeof(UnityEngine.Experimental.PlayerLoop.Update))]
    public class TweenPositionSystem : JobComponentSystem
    {
        struct TweenGroup
        {
            public ComponentDataArray<Position> positions;
            [ReadOnly]
            public ComponentDataArray<TweenTarget> target;
            [ReadOnly]
            public ComponentDataArray<TweenTime> tweenTime;

            public int Length;
        }

        [Inject] private TweenGroup m_Tweens;

        [BurstCompile]
        struct TweenJob : IJobParallelFor
        {
            public ComponentDataArray<Position> positions;
            [ReadOnly]
            public ComponentDataArray<TweenTarget> target;
            [ReadOnly]
            public ComponentDataArray<TweenTime> tweenTime;

            public float dt;

            public void Execute(int i)
            {
                positions[i] = new Position
                {
                    Value = math.lerp(target[i].From, target[i].To, tweenTime[i].Value)
                };
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new TweenJob()
            {
                positions = m_Tweens.positions,
                target = m_Tweens.target,
                tweenTime = m_Tweens.tweenTime,
                dt = Time.deltaTime
            };

            return job.Schedule(m_Tweens.Length, 64, inputDeps);
        }
    }
}
