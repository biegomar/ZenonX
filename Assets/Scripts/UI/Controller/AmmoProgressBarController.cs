using UnityEngine;
using UnityEngine.UI;

namespace UI.Controller
{
    /// <summary>
    /// The ammo progress bar controller.
    /// </summary>
    public class AmmoProgressBarController : AProgressBarController
    {
        private void Start()
        {
            this.slider.maxValue = this.GetActualBaseMaxValue();
            this.fill.color = this.gradient.Evaluate(1f);
        }
        
        protected override float GetActualBaseValue()
        {
            return GameManager.Instance.ActualShipLaserPower;
        }
        
        protected override float GetActualBaseMaxValue()
        {
            return GameManager.Instance.MaxShipLaserPower;
        }
    }
}
