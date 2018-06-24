using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using UnityEngine;

namespace ECSTween
{
    // Removes all entities ( not gameObjects ) that have TweenRange beyond their lifetime
    [UpdateInGroup(typeof(TweenTimeUpdateGroup))]
    [UpdateAfter(typeof(UnityEngine.Experimental.PlayerLoop.PostLateUpdate))]
    public class TweenRemoveSystem : ComponentSystem
    {
        public struct Data
        {
            public int Length;
            [ReadOnly] public EntityArray Entities;
            [ReadOnly] public ComponentDataArray<TweenLifetime> Ranges;
        }

        [Inject] private Data m_Data;

        protected override void OnUpdate()
        {
            var time = Time.time;
            for (int i = 0; i < m_Data.Length; ++i)
            {
                var timeRange = m_Data.Ranges[i];

                if (time > (timeRange.StartTime + timeRange.Lifetime))
                {
                    PostUpdateCommands.DestroyEntity(m_Data.Entities[i]);
                }
            }
        }
    }
}