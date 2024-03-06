using UnityEngine;
using UnityEngine.UI;

namespace UI.Controller
{
    public abstract class AProgressBarController : MonoBehaviour
    {
        [SerializeField] protected Slider slider;
        [SerializeField] protected Gradient gradient;
        [SerializeField] protected Image fill;
        
        public void SetSliderValue()
        {
            this.slider.maxValue = this.GetActualBaseMaxValue();
            this.slider.value = this.GetActualBaseValue();
            this.fill.color = this.gradient.Evaluate(this.slider.normalizedValue);
        }

        protected abstract float GetActualBaseValue();

        protected abstract float GetActualBaseMaxValue();
    }
}