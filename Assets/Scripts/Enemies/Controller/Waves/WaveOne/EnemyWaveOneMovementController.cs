using System;
using System.Collections.Generic;
using System.Linq;
using Enemies.Model;
using Enemies.Services;
using UnityEngine;

namespace Enemies.Controller.Waves.WaveOne
{
    public class EnemyWaveOneMovementController : MonoBehaviour
    {
        private const float hitInterval = 6f;
        private static float timeSinceLastHit = 0f;

        private EnemyWaveOneSpawnController enemyController;
        private EnemyFlightFormationItem enemyItem;
        private Vector2 startPosition;
        private IMovementStrategy activeMovementStrategy;    
    
        private bool isNegativeXDirection;


        void Start()
        {        
            GameObject go = GameObject.Find("EnemyWaveOne");
            if (go != null)
            {
                this.enemyController = go.GetComponent<EnemyWaveOneSpawnController>();
                if (this.enemyController != null)
                {
                    this.enemyItem = this.enemyController.Enemies[gameObject.GetInstanceID()];
                    this.isNegativeXDirection = this.enemyController.EnemyFlightFormationNegativeDirection[this.enemyItem.WaveId];
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

            this.startPosition = transform.position;
            this.activeMovementStrategy = new SinusMovement(this.startPosition);
        }

        private void Update()
        {
            // use delta time for game pause here.
            if (GameManager.Instance.IsGameRunning && Time.deltaTime > 0f)
            {
                timeSinceLastHit += Time.deltaTime;

                transform.position = new Vector3(
                    CalculateNewXPosition(),
                    CalculateNewYPosition(),
                    transform.position.z);

                TryToSwitchToSinusMovement();
            }        
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {        
            var collisionObject = collision.gameObject;
            switch (collisionObject.tag)
            {
                case "PlayerLaser":
                {
                    if (enemyItem != null)
                    {
                        Destroy(collisionObject);
                        enemyItem.Health = enemyItem.Health - 1;
                        if (enemyItem.Health <= 0)
                        {
                            var lastPosition = transform.position;                            
                            
                            RemoveEnemyAndScore();

                            enemyController.SpawnLoot(lastPosition);
                        }
                    }
                    
                    break;
                }
                case "Border":
                    activeMovementStrategy = new DirectMovement(startPosition, gameObject, this.isNegativeXDirection);
                    this.isNegativeXDirection = !this.isNegativeXDirection;
                    break;
                case "Player":
                    if (timeSinceLastHit > hitInterval)
                    {
                        GameManager.Instance.ActualShipHealth -= 5;
                        timeSinceLastHit = 0f;

                        var lastPosition = transform.position;

                        RemoveEnemyAndScore();

                        enemyController.SpawnLoot(lastPosition);
                    }
                    break;
                case "SpaceShipShield":
                    if (timeSinceLastHit > hitInterval)
                    {
                        GameManager.Instance.ActualShieldHealth -= 5;
                        timeSinceLastHit = 0f;

                        var lastPosition = transform.position;

                        RemoveEnemyAndScore();

                        enemyController.SpawnLoot(lastPosition);
                    }
                    break;
            }
        }

        private void RemoveEnemyAndScore()
        {
            RemoveEnemyFromWave(enemyController.EnemyFlightFormation);
            Destroy(gameObject);
            GameManager.Instance.Score += GameManager.Instance.EnemyWaveOneScore;
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

        private void TryToSwitchToSinusMovement()
        {
            var waveId = this.enemyController.EnemyFlightFormation.Values.SelectMany(x => x).Where(x => x.Enemy == gameObject).Select(x => x.WaveId).SingleOrDefault();

            if (waveId != null && !this.enemyController.EnemyFlightFormation[waveId].Where(x => x.StartPosition.y != x.Enemy.transform.position.y).Any())
            {
                this.startPosition = transform.position;
                this.activeMovementStrategy = new SinusMovement(this.startPosition);
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
    }
}
