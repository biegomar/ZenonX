using UI.Controller;
using UnityEngine;

namespace Player.Controller
{
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
