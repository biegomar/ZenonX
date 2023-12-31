using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IncentiveController
{
    private static bool isFirstIncentive = false;
    private static bool isSecondIncentive = false;
    private static bool isThirdIncentive = false;    

    public static void GiveIncentive()
    {       
        if (!isFirstIncentive)
        {
            isFirstIncentive = true;
            GameManager.Instance.Level1LaserFrequence = 0.2f;
        }
        else if (isFirstIncentive && !isSecondIncentive)
        {
            isSecondIncentive = true;
            GameManager.Instance.MaxShipLaserPower = 75;
            GameManager.Instance.ActualLaserPower = GameManager.Instance.MaxShipLaserPower;            

        }
        else if (isSecondIncentive && !isThirdIncentive)
        {
            isSecondIncentive = true;
            GameManager.Instance.MaxShipLaserPower = 75;
            GameManager.Instance.ActualLaserPower = GameManager.Instance.MaxShipLaserPower;
        }

    }
}
