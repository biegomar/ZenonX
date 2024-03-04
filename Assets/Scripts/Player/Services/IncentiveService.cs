using System;
using System.Collections.Generic;
using System.Linq;

namespace Player.Services
{
    /// <summary>
    /// The central service to manage the incentives.
    /// </summary>
    public class IncentiveService
    {
        private static IncentiveService instance;
        private static readonly object lockObject = new object();

        private IDictionary<byte, Action> incentives;
        private IDictionary<byte, string> incentiveMessages;
        private const byte ShieldIndex = 3;
        private const byte LaserFrequencyIndex = 5;

        private IncentiveService()
        {
            InitializeIncentives();
            InitializeIncentiveMessages();
        }

        public static IncentiveService Instance 
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new IncentiveService();
                        }
                    }
                }
                return instance;    
            }
        }
        

        private void InitializeIncentives()
        {
            this.incentives = new Dictionary<byte, Action>();
        
            this.incentives.Add(1, this.GetMoreMaxLaserPower);
            this.incentives.Add(2, this.GetMoreMaxHealthPoints);
            this.incentives.Add(ShieldIndex, this.GetSpaceShipShield);
            this.incentives.Add(4, this.GetHigherSpaceShipShield);
            this.incentives.Add(LaserFrequencyIndex, this.GetHigherLaserFrequence);
        }
        
        private void InitializeIncentiveMessages()
        {
            this.incentiveMessages = new Dictionary<byte, string>();
        
            this.incentiveMessages.Add(1, "more ammo");
            this.incentiveMessages.Add(2, "more health points");
            this.incentiveMessages.Add(ShieldIndex, "shield");
            this.incentiveMessages.Add(4, "stronger shield");
            this.incentiveMessages.Add(LaserFrequencyIndex, "update laser frequency");
        }

        public void GiveIncentive()
        {
            TryToReAddShieldInIncentives();
            
            byte index;
            do
            {
                index = (byte)UnityEngine.Random.Range(1, this.incentives.Keys.Max() + 1);
            } while (!this.incentives.ContainsKey(index));

            GameManager.Instance.LootMessage = this.incentiveMessages[index];
            this.incentives[index].Invoke();
            GameManager.Instance.IsLootSpawned = true;
        }

        private void TryToReAddShieldInIncentives()
        {   
            if (!GameManager.Instance.IsShipShieldActive && !this.incentives.ContainsKey(ShieldIndex))
            {
                this.incentives.Add(ShieldIndex, this.GetSpaceShipShield);
            }
        }

        private void GetMoreMaxHealthPoints()
        {
            GameManager.Instance.MaxShipHealth++;
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
                this.incentives.Remove(ShieldIndex);
            }

            GameManager.Instance.ActualShieldHealth = GameManager.Instance.MaxShieldHealth;
        }
        
        private void GetHigherSpaceShipShield()
        {
            GameManager.Instance.MaxShieldHealth++;
            GameManager.Instance.ActualShieldHealth = GameManager.Instance.MaxShieldHealth;
        }
    }
}
