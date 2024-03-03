using UI.Controller;
using UnityEngine;

namespace Player.Controller
{
    /// <summary>
    /// The space ship health controller.
    /// </summary>
    public class SpaceShipHealthController : MonoBehaviour
    {
        [SerializeField]
        private HealthProgressBarController healthProgressBarController;
    
        private void Update()
        {
            this.healthProgressBarController.SetHealthValue();
        
            if (GameManager.Instance.ActualShipHealth <= 0)
            {
                GameManager.Instance.IsGameRunning = false;
            }
        }
    }
}
