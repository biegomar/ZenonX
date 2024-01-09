using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthProgressBarController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        this.slider.maxValue = GameManager.Instance.MaxShipHealth;
    }

    public void SetHealthValue()
    {
        var percentage = this.GetActualHealthValue();
        this.slider.value = percentage;
    }

    private int GetActualHealthValue()
    {
        this.slider.maxValue = GameManager.Instance.MaxShipHealth;
        return GameManager.Instance.ActualShipHealth;
    } 

}
