using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShieldHealthController : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.ActualShieldHealth <= 0f)
        {
            GameManager.Instance.IsShipShieldActive = false;
        }
    }
}
