using System;
using Unity.Entities;
using Unity.Mathematics;

namespace ECSTween
{
    public struct TweenTime : IComponentData
    {
        public float Value;
    }

    public struct TweenLifetime : IComponentData
    {
        public float StartTime;
        public float Lifetime;
    }

    public struct TweenPosition : IComponentData
    {
        public float3 From;
        public float3 To;
    }

    public struct TweenRotation : IComponentData
    {
        public quaternion From;
        public quaternion To;
    }

    public struct TweenComplete : IComponentData { }

    // Easings
    struct TweenEasingExpIn : IComponentData { }
    struct TweenEasingExpOut : IComponentData { }
}