using System.Threading;
using UI.Controller;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Controller.Laser
{
    /// <summary>
    /// The fire controller for the space ship.
    /// </summary>
    public class FireController : MonoBehaviour
    {
        [SerializeField]
        private GameObject Laser;

        [SerializeField]
        private AmmoProgressBarController ammoProgressBarController;

        private float ActualLaserFrequence;
        private float LaserInterval;
        private float ActualLaserPowerRegainInterval;  
        private AudioSource laserSound;

        public void Start()
        {
            this.laserSound = AudioManager.Instance.GetSound("PlayerLaser");
            this.LaserInterval = 0;
            this.ActualLaserPowerRegainInterval = 0;
        }

        public void Update()
        {
            // use delta time for game pause here.
            if (GameManager.Instance.IsGameRunning && Time.deltaTime > 0f)
            {
                this.ActualLaserFrequence = GameManager.Instance.ShipLaserFrequency;
                this.LaserInterval += Time.deltaTime;
                this.ActualLaserPowerRegainInterval += Time.deltaTime;
                
                this.GainFirePower();
                this.ammoProgressBarController.SetSliderValue();
            }        
        }

        private void GainFirePower()
        {
            if (this.ActualLaserPowerRegainInterval >= GetLaserPowerRegainIntervalHalfLife())
            {
                if (GameManager.Instance.ActualShipLaserPower < GameManager.Instance.MaxShipLaserPower)
                {
                    GameManager.Instance.ActualShipLaserPower++;
                    this.ActualLaserPowerRegainInterval = 0;
                }
            }                  
        }

        private float GetLaserPowerRegainIntervalHalfLife()
        {
            var percentage = ((float)GameManager.Instance.ActualShipLaserPower / GameManager.Instance.MaxShipLaserPower);
            return percentage switch
            {
                >= .5f => GameManager.Instance.LaserPowerRegainInterval,
                >= .25f and < .5f => GameManager.Instance.LaserPowerRegainInterval / 2,
                >= .1f and < .25f => GameManager.Instance.LaserPowerRegainInterval / 3,
                < .1f => GameManager.Instance.LaserPowerRegainInterval / 4,
                _ => throw new System.NotImplementedException()
            };
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed && this.LaserInterval >= this.ActualLaserFrequence && GameManager.Instance.ActualShipLaserPower > 0)
            {
                this.laserSound.enabled = true;
                this.laserSound.Play();

                Instantiate(Laser, new Vector3(
                    transform.position.x,
                    transform.position.y + 0.3f,
                    transform.position.z), Quaternion.identity);            

                GameManager.Instance.ActualShipLaserPower--;
                this.LaserInterval = 0;                
            }
        }
    }
}
