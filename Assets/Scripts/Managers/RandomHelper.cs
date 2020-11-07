using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomHelper
{
    public static int RandomIntLessThan(int max)
    {
        return Random.Range(0, max);
    }
}
