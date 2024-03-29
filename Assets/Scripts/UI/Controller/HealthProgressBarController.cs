using UnityEngine;
using UnityEngine.UI;

namespace UI.Controller
{
    /// <summary>
    /// The health progress bar controller.
    /// </summary>
    public class HealthProgressBarController : AProgressBarController
    {
        private void Start()
        {
            this.slider.maxValue = this.GetActualBaseMaxValue();
            this.fill.color = this.gradient.Evaluate(1f);
        }
        
        protected override float GetActualBaseValue()
        {
            return GameManager.Instance.ActualShipHealth;
        }
        
        protected override float GetActualBaseMaxValue()
        {
            return GameManager.Instance.MaxShipHealth;
        }
    }
}
