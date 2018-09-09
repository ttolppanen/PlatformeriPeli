using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions {

    public static float SecondDegreeSolver(float a, float b, float c, bool plusOrMinus)
    {
        if (plusOrMinus)
        {
            return (-b + Mathf.Sqrt(Mathf.Pow(b, 2) - 4 * a * c)) / (2 * a);
        }
        else
        {
            return (-b - Mathf.Sqrt(Mathf.Pow(b, 2) - 4 * a * c)) / (2 * a);
        }
    }
}
