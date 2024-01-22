using UnityEngine;

namespace Player.Controller.Extensions
{
    public class SpaceShipShieldHealthController : MonoBehaviour
    {
        void Update()
        {
            if (GameManager.Instance.ActualShieldHealth <= 0f)
            {
                GameManager.Instance.IsShipShieldActive = false;
            }
        }
    }
}
