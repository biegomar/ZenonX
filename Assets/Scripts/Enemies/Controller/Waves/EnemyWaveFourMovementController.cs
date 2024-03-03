using System;
using System.Collections.Generic;
using System.Linq;
using Enemies.Model;
using Enemies.Services;
using Enemies.Services.Formations;
using Enemies.Services.Movements;
using UnityEngine;

namespace Enemies.Controller.Waves.WaveFour
{
    /// <summary>
    /// The movement controller for enemy wave four.
    /// </summary>
    public class EnemyWaveFourMovementController : MonoBehaviour
    {
        private IMovementStrategy activeMovementStrategy;
        private WaveSpawnController enemyController;
        private EnemyFlightFormationItem enemyItem;
        private EnemyFormation formation;
        private Guid formationId;

        private float waitTimer; 
    
        private void Start()
        {
            this.waitTimer = 0;
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

            this.activeMovementStrategy = new CycloidMovement(this.enemyItem.StartPosition, this.enemyItem);    
        }
    
        private void Update()
        {
            this.waitTimer += Time.deltaTime;
            
            // use delta time for game pause here.
            if (GameManager.Instance.IsGameRunning && Time.deltaTime > 0f)
            {
                //wait before you move at all
                if (this.waitTimer > this.enemyItem.PositionInFormation * GameManager.Instance.EnemyWaveFourTimeDistanceBeforeMovement)
                {
                    if ((!this.enemyItem.Flag && transform.position.x > 9) ||
                        (this.enemyItem.Flag && transform.position.x < -9))
                    {
                        this.enemyItem.Flag = !this.enemyItem.Flag;
                        //this.RemoveEnemyAndScore(false);
                        //enemyController.SpawnLoot(new Vector3(0,0,-11f));
                    }
                    else
                    {
                        transform.position = new Vector3(
                            CalculateNewXPosition(),
                            CalculateNewYPosition(),
                            transform.position.z);   
                    } 
                }
            }
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

                        enemyController.SpawnLoot(this.formationId,
                            this.formation.enemyFormationData.LootTemplate, lastPosition);
                    }
                }

                Destroy(collisionObject);
            }
        }
    
        private void RemoveEnemyAndScore(bool reallyScore = true)
        {
            RemoveEnemyFromWave(enemyController.EnemyFlightFormations);
            Destroy(gameObject);
            if (reallyScore)
            {
                GameManager.Instance.Score += GameManager.Instance.EnemyWaveFourScore;    
            }
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
    }
}
