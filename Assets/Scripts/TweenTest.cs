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
            Tween.MovePosition(item.gameObject, dest, Random.Range(5f, 20f), GetRandomEasingType());
            Tween.Rotation(item.gameObject, Random.rotation, Quaternion.identity, Random.Range(5f, 20f), GetRandomEasingType());

            //Tween.Position(item.gameObject, target, 10f);
            //Tween.Position(item.gameObject, new Vector3(-10f, 0, 0), new Vector3(10f, 0, 0), 10f);
            //Tween.MovePosition(item.gameObject, new Vector3(10f, 0, 0), 10f);
        }
    }

    private EasingType GetRandomEasingType()
    {
        var rdm = Random.value;

        if (rdm > 0.7f)
        {
            return EasingType.ExpOut;
        }
        else if (rdm > 0.4f)
        {
            return EasingType.ExpIn;
        }
        else
        {
            return EasingType.Linear;
        }
    }
}
