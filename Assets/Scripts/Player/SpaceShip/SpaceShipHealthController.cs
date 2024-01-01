using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipHealthController : MonoBehaviour
{
    [SerializeField]
    private HealthViewModel healthViewModel;


    private void Update()
    {
        this.healthViewModel.SetHealthValue();
        
        if (GameManager.Instance.ActualShipHealth == 0)
        {
            GameManager.Instance.IsGameRunning = false;
        }
    }
}
