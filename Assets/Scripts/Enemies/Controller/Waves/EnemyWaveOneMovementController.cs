using System;
using System.Collections.Generic;
using System.Linq;
using Enemies.Model;
using Enemies.Services;
using Enemies.Services.Formations;
using Enemies.Services.Movements;
using UnityEngine;

namespace Enemies.Controller.Waves.WaveOne
{
    public class EnemyWaveOneMovementController : MonoBehaviour
    {
        private const float hitInterval = 6f;
        private static float timeSinceLastHit = 0f;

        private WaveSpawnController enemyController;
        private EnemyFlightFormationItem enemyItem;
        private EnemyFormation formation;
        private Guid formationId;
        private Vector2 startPosition;
        private IMovementStrategy activeMovementStrategy;    

        private bool isSinusWaveYDirectionPositiv;

        void Start()
        {
            this.enemyController = GameManager.FindObjectInParentChain<WaveSpawnController>(this.transform);
            if (this.enemyController != null)
            {
                this.enemyItem = this.enemyController.Enemies[gameObject.GetInstanceID()];
                if (this.enemyItem != null)
                {
                    this.formation = this.enemyItem.Formation;
                    this.formationId = this.enemyItem.FormationId;
                }
            }

            this.startPosition = transform.position;
            this.isSinusWaveYDirectionPositiv = false;
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

                if (transform.position.y >= 4.5f)
                {
                    this.isSinusWaveYDirectionPositiv = false;
                }
                else if(transform.position.y <= -3f)
                {
                    this.isSinusWaveYDirectionPositiv = true;
                }
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

                            enemyController.SpawnLoot(this.formationId,
                                this.formation.enemyFormationData.LootTemplate, lastPosition);
                        }
                    }
                    break;
                }
                case "Player":
                    if (timeSinceLastHit > hitInterval)
                    {
                        GameManager.Instance.ActualShipHealth -= 5;
                        timeSinceLastHit = 0f;

                        var lastPosition = transform.position;

                        RemoveEnemyAndScore();

                        enemyController.SpawnLoot(this.formationId,
                            this.formation.enemyFormationData.LootTemplate, lastPosition);
                    }
                    break;
                case "SpaceShipShield":
                    if (timeSinceLastHit > hitInterval)
                    {
                        GameManager.Instance.ActualShieldHealth -= 5;
                        timeSinceLastHit = 0f;

                        var lastPosition = transform.position;

                        RemoveEnemyAndScore();

                        enemyController.SpawnLoot(this.formationId,
                            this.formation.enemyFormationData.LootTemplate, lastPosition);
                    }
                    break;
            }
        }

        private void RemoveEnemyAndScore()
        {
            RemoveEnemyFromWave(enemyController.EnemyFlightFormations);
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

        private float CalculateNewXPosition()
        {
            return this.activeMovementStrategy.CalculateNewXPosition(gameObject, this.isSinusWaveYDirectionPositiv);        
        }

        private float CalculateNewYPosition()
        {        
            return this.activeMovementStrategy.CalculateNewYPosition(gameObject, this.isSinusWaveYDirectionPositiv);
        }
    }
}
