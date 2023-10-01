using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringTime
{
    public static string SecondToTimeString(float second)
    {
        return TimeSpan.FromSeconds(second).ToString(@"mm\:ss\.ff");
    }
}
