using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IncentiveController
{
    private static bool isFirstIncentive = false;

    public static void GiveIncentive()
    {
        if (!isFirstIncentive)
        {
            isFirstIncentive = true;
            GameManager.Instance.Level1LaserFrequence = 0.2f;
        }
    }
}
