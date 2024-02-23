using System;
using System.Collections.Generic;

namespace Player.Services
{
    public class IncentiveService
    {
        private readonly IDictionary<byte, Action> incentives;
        private const byte LaserFrequencyIndex = 4;

        public IncentiveService()
        {
            this.incentives = new Dictionary<byte, Action>();
        
            this.incentives.Add(1, this.GetMoreMaxLaserPower);
            this.incentives.Add(2, this.GetMoreMaxHealthPoints);
            this.incentives.Add(3, this.GetSpaceShipShield);
            this.incentives.Add(LaserFrequencyIndex, this.GetHigherLaserFrequence);
        }
    
        public void GiveIncentive()
        {
            byte index;
            do
            {
                index = (byte)UnityEngine.Random.Range(1, this.incentives.Count+1);    
            } while (!this.incentives.ContainsKey(index));
        
            this.incentives[index].Invoke();
        }

        private void GetMoreMaxHealthPoints()
        {
            GameManager.Instance.MaxShipHealth += 1;
            GameManager.Instance.ActualShipHealth = GameManager.Instance.MaxShipHealth;
        }

        private void GetMoreMaxLaserPower()
        {
            GameManager.Instance.MaxShipLaserPower += 5;
            GameManager.Instance.ActualShipLaserPower = GameManager.Instance.MaxShipLaserPower;
        }

        private void GetHigherLaserFrequence()
        {
            GameManager.Instance.ShipLaserFrequency /= 2;
            if (GameManager.Instance.ShipLaserFrequency <= 0.1f)
            {
                this.incentives.Remove(LaserFrequencyIndex);
            }
        }

        private void GetSpaceShipShield()
        {
            if (!GameManager.Instance.IsShipShieldActive)
            {
                GameManager.Instance.IsShipShieldActive = true;
            }

            GameManager.Instance.ActualShieldHealth = GameManager.Instance.MaxShieldHealth;
        }
    }
}
