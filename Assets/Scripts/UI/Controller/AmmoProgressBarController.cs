using UnityEngine;
using UnityEngine.UI;

namespace UI.Controller
{
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
            var percentage = this.GetActualLaserPowerValue();
            this.slider.value = percentage;        
        }    

        private float GetActualLaserPowerValue()
        {
            this.slider.maxValue = GameManager.Instance.MaxShipLaserPower;        

            return (float)GameManager.Instance.ActualShipLaserPower;
        }
    }
}
