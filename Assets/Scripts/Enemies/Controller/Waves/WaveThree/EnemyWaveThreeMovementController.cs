using System;
using System.Collections.Generic;
using System.Linq;
using Enemies.Model;
using Enemies.Services;
using Enemies.Services.Movements;
using UnityEngine;

namespace Enemies.Controller.Waves.WaveThree
{
    public class EnemyWaveThreeMovementController : MonoBehaviour
    {
        private EnemyWaveThreeSpawnController enemyController;
        private EnemyFlightFormationItem enemyItem;
        private IMovementStrategy activeMovementStrategy;  
    
        void Start()
        {        
            GameObject go = GameObject.Find("EnemyWaveThree");
            if (go != null)
            {
                this.enemyController = go.GetComponent<EnemyWaveThreeSpawnController>();
                if (this.enemyController != null)
                {
                    this.enemyItem = this.enemyController.Enemies[gameObject.GetInstanceID()];
                }
                else
                {
                    Debug.Log("go.GetComponent<EnemyController>() is null");
                }
            }
            else
            {
                Debug.Log("GameObject.Find(Enemies) is null");
            }

            this.activeMovementStrategy = new XPingPongLerpMovement(this.enemyItem.StartPosition);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            var collisionObject = collision.gameObject;
        
            if (collisionObject.CompareTag("PlayerLaser"))
            {
                if (enemyItem != null)
                {
                    enemyItem.Health -= 1;
                    if (enemyItem.Health <= 0)
                    {
                        var lastPosition = transform.position;

                        RemoveEnemyAndScore();

                        enemyController.SpawnLoot(lastPosition);
                    }
                }

                Destroy(collisionObject);
            }
        }

        private void Update()
        {
            // use delta time for game pause here.
            if (GameManager.Instance.IsGameRunning && Time.deltaTime > 0f)
            {
                transform.position = new Vector3(
                    CalculateNewXPosition(),
                    CalculateNewYPosition(),
                    transform.position.z);
            }

            TryToSwitchToXPingPongMovement();
        }
    
        private void RemoveEnemyAndScore()
        {
            RemoveEnemyFromWave(enemyController.EnemyFlightFormation);
            Destroy(gameObject);
            GameManager.Instance.Score += GameManager.Instance.EnemyWaveThreeScore;
        }

        private void RemoveEnemyFromWave(IDictionary<Guid, IList<EnemyFlightFormationItem>> enemyWaves)
        {
            foreach (var wave in enemyWaves)
            {
                var enemyFlightFormationItem = wave.Value.FirstOrDefault(item => item.Enemy == gameObject);
                if (enemyFlightFormationItem != null)
                {
                    wave.Value.Remove(enemyFlightFormationItem);
                }                                   
            }
        }
    
        private float CalculateNewXPosition()
        {
            return this.activeMovementStrategy.CalculateNewXPosition(gameObject);        
        }

        private float CalculateNewYPosition()
        {        
            return this.activeMovementStrategy.CalculateNewYPosition(gameObject);
        }
    
        private void TryToSwitchToXPingPongMovement()
        {
            if (this.activeMovementStrategy.GetType() != typeof(XPingPongMovement))
            {
                if (Math.Abs(transform.position.x - this.enemyItem.StartPosition.x) < 0.03f)
                {
                    this.activeMovementStrategy = new XPingPongMovement(this.enemyItem.StartPosition);
                } 
            }
        }
    }
}
