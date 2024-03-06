namespace UI.Controller
{
    public class ShieldHealthProgressBarController : AProgressBarController
    {
        private void Start()
        {
            this.slider.maxValue = this.GetActualBaseMaxValue();
            this.fill.color = this.gradient.Evaluate(1f);
        }
        
        protected override float GetActualBaseValue()
        {
            return GameManager.Instance.ActualShieldHealth;
        }
        
        protected override float GetActualBaseMaxValue()
        {
            return GameManager.Instance.MaxShieldHealth;
        }
    }
}
