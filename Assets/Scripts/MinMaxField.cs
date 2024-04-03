using System;
using Random = UnityEngine.Random;

[Serializable]
public struct MinMaxFloat
{
     public float Min;
     public float Max;

     public float GetRandomInRange()
     {
          return Random.Range(Min, Max);
     }
}