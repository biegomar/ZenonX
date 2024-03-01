using System;
using System.Collections.Generic;

namespace Player.Services
{
    public class IncentiveService
    {
        private IDictionary<byte, Action> incentives;
        private IDictionary<byte, string> incentiveMessages;
        private const byte LaserFrequencyIndex = 4;

        public IncentiveService()
        {
            InitializeIncentives();
            InitializeIncentiveMessages();
        }

        private void InitializeIncentives()
        {
            this.incentives = new Dictionary<byte, Action>();
        
            this.incentives.Add(1, this.GetMoreMaxLaserPower);
            this.incentives.Add(2, this.GetMoreMaxHealthPoints);
            this.incentives.Add(3, this.GetSpaceShipShield);
            this.incentives.Add(LaserFrequencyIndex, this.GetHigherLaserFrequence);
        }
        
        private void InitializeIncentiveMessages()
        {
            this.incentiveMessages = new Dictionary<byte, string>();
        
            this.incentiveMessages.Add(1, "update maximum ammo");
            this.incentiveMessages.Add(2, "more health points");
            this.incentiveMessages.Add(3, "shield");
            this.incentiveMessages.Add(LaserFrequencyIndex, "update laser frequency");
        }

        public void GiveIncentive()
        {
            byte index;
            do
            {
                index = (byte)UnityEngine.Random.Range(1, this.incentives.Count+1);    
            } while (!this.incentives.ContainsKey(index));

            GameManager.Instance.LootMessage = this.incentiveMessages[index];
            this.incentives[index].Invoke();
            GameManager.Instance.IsLootSpawned = true;
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
