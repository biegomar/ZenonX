using UnityEngine;
using UnityEngine.UI;

namespace UI.Controller
{
    /// <summary>
    /// The health progress bar controller.
    /// </summary>
    public class HealthProgressBarController : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image fill;

        private void Start()
        {
            this.slider.maxValue = GameManager.Instance.MaxShipHealth;
            this.fill.color = this.gradient.Evaluate(1f);
        }

        public void SetHealthValue()
        {
            var percentage = this.GetActualHealthValue();
            this.slider.value = percentage;
            this.fill.color = this.gradient.Evaluate(this.slider.normalizedValue);
        }

        private int GetActualHealthValue()
        {
            this.slider.maxValue = GameManager.Instance.MaxShipHealth;
            return GameManager.Instance.ActualShipHealth;
        } 

    }
}
