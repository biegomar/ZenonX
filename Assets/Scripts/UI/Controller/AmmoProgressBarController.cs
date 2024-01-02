using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoProgressBarController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        this.slider.maxValue = GameManager.Instance.MaxShipLaserPower;
    }

    public void SetAmmoValue()
    {
        var percentage = this.GetActualaserPowerValue();
        this.slider.value = percentage;        
    }    

    private float GetActualaserPowerValue()
    {
        this.slider.maxValue = GameManager.Instance.MaxShipLaserPower;        

        return (float)GameManager.Instance.ActualShipLaserPower;
    }
}
