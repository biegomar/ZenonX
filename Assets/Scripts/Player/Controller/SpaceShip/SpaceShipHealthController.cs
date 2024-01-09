using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpaceShipHealthController : MonoBehaviour
{
    [SerializeField]
    private HealthProgressBarController healthProgressBarController;
    
    private void Update()
    {
        this.healthProgressBarController.SetHealthValue();
        
        if (GameManager.Instance.ActualShipHealth <= 0)
        {
            GameManager.Instance.IsGameRunning = false;
        }
    }
}
