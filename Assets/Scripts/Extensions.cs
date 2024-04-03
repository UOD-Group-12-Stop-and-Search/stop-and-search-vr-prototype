using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public static class Extensions
{
    public static float GetRandomTime(this AnimationCurve animationCurve)
    {
        // get time of first and last elements
        float firstTime = animationCurve.keys[0].time;
        float finalTime = animationCurve.keys[animationCurve.keys.Length - 1].time;

        // choose from random between min and max times
        return Random.Range(firstTime, finalTime);
    }

    public static T GetRandomElement<T>(this IReadOnlyList<T> collection)
    {
        return collection[Mathf.RoundToInt(Random.value * (collection.Count - 1))];
    }
}