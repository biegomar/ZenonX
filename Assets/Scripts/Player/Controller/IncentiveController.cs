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
            GetHigherLaserFrequence();
        }
        else if (isFirstIncentive && !isSecondIncentive)
        {
            GetMoreMaxHealthPoints();            
        }
        else if (isSecondIncentive && !isThirdIncentive)
        {
            GetMoreMaxLaserPower();
        }

    }

    private static void GetMoreMaxHealthPoints()
    {
        isSecondIncentive = true;
        GameManager.Instance.MaxShipHealth += 5;
        GameManager.Instance.ActualShipHealth = GameManager.Instance.MaxShipHealth;
    }

    private static void GetMoreMaxLaserPower()
    {
        isSecondIncentive = true;
        GameManager.Instance.MaxShipLaserPower = 75;
        GameManager.Instance.ActualShipLaserPower = GameManager.Instance.MaxShipLaserPower;
    }

    private static void GetHigherLaserFrequence()
    {
        isFirstIncentive = true;
        GameManager.Instance.Level1LaserFrequence = 0.2f;
    }
}
