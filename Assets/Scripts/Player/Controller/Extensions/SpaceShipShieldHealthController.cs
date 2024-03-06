using UI.Controller;
using UnityEngine;

namespace Player.Controller.Extensions
{
    /// <summary>
    /// A special controller for the space ship shield health.
    /// </summary>
    public class SpaceShipShieldHealthController : MonoBehaviour
    {
        [SerializeField] private ShieldHealthProgressBarController shieldHealthProgressBarController;
        void Update()
        {
            if (GameManager.Instance.IsShipShieldActive)
            {
                this.shieldHealthProgressBarController.SetSliderValue();
            
                if (GameManager.Instance.ActualShieldHealth <= 0f)
                {
                    GameManager.Instance.IsShipShieldActive = false;
                }  
            }
        }
    }
}
