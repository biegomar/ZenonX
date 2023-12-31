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

    public void SetHealthValue()
    {
        var percentage = this.GetActualHealthValue();
        this.slider.value = percentage;        
    }    

    private float GetActualHealthValue()
    {
        this.slider.maxValue = GameManager.Instance.MaxShipLaserPower;        

        return (float)GameManager.Instance.ActualLaserPower;
    }
}
