using UnityEngine;

namespace Player.Controller.Extensions
{
    /// <summary>
    /// A special controller for the space ship shield health.
    /// </summary>
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
