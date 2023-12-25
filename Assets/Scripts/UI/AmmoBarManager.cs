using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBarManager : MonoBehaviour
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
        Debug.Log($"Ammo: {percentage}");
    }    

    private float GetActualHealthValue()
    {
        this.slider.maxValue = GameManager.Instance.MaxShipLaserPower;
        var percentage = ((float)GameManager.Instance.ActualLaserPower / GameManager.Instance.MaxShipLaserPower);

        var actualHealthValue = (float)this.slider.maxValue * percentage;

        return actualHealthValue;
    }
}
