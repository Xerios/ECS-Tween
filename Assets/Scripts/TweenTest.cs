using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using ECSTween;

public class TweenTest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var entityManager = World.Active.GetExistingManager<EntityManager>();
        var dest = new Vector3(20f, 0, 0);

        foreach (Transform item in transform)
        {
            Tween.MovePosition(item.gameObject, dest, Random.Range(5f, 20f), Random.value > 0.5f ? EasingType.Linear : EasingType.ExpIn);

            //Tween.Rotation(item.gameObject, Quaternion.identity, Random.rotation, Random.Range(5f, 20f), Random.value > 0.5f ? EasingType.Linear : EasingType.ExpIn);

            //Tween.Position(item.gameObject, target, 10f);
            //Tween.Position(item.gameObject, new Vector3(-10f, 0, 0), new Vector3(10f, 0, 0), 10f);
            //Tween.MovePosition(item.gameObject, new Vector3(10f, 0, 0), 10f);
        }
    }
}
