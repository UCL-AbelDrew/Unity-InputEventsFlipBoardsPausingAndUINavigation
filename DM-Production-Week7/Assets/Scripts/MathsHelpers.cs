using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsHelpers
{
    public float NormaliseValue(float min,float max,float val)
    {
        float value = (val - min) / (max - min);
        return value;
    }
}
