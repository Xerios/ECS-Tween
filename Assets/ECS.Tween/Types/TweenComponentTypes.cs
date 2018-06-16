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

    // Easings
    struct TweenEasingExpIn : IComponentData { }
}