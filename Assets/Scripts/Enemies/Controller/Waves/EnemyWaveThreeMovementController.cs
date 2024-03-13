using System;
using System.Collections.Generic;
using System.Linq;
using Enemies.Model;
using Enemies.Services.Formations;
using Enemies.Services.Movements;
using UnityEngine;

namespace Enemies.Controller.Waves
{
    /// <summary>
    /// The movement controller for enemy wave three.
    /// </summary>
    public class EnemyWaveThreeMovementController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private WaveSpawnController enemyController;
        private EnemyFlightFormationItem enemyItem;
        private EnemyFormation formation;
        private Guid formationId;
        private IMovementStrategy activeMovementStrategy;  
        
        private static readonly int AmIDead = Animator.StringToHash("AmIDead");
        private bool IAmDying;
        private bool InCollision;
    
        void Start()
        {
            this.IAmDying = false;
            this.InCollision = false;
            
            this.enemyController = GameManager.FindObjectInParentChain<WaveSpawnController>(this.transform);
            if (this.enemyController != null)
            {
                this.enemyItem = this.enemyController.Enemies[gameObject.GetInstanceID()];
                if (this.enemyItem != null)
                {
                    this.formation = this.enemyItem.Formation;
                    this.formationId = this.enemyItem.FormationId;
                    
                    this.activeMovementStrategy = new XPingPongLerpMovement(this.enemyItem.StartPosition);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (enemyItem != null && !this.IAmDying && !this.InCollision)
            {
                this.InCollision = true;
                
                var lastPosition = transform.position;
                var collisionObject = collision.gameObject;
            
                switch (collisionObject.tag)
                {
                    case "PlayerLaser":
                    {
                        Destroy(collisionObject);
                        enemyItem.Health -= 1;
                        if (enemyItem.Health <= 0)
                        {
                            RemoveEnemyAndScore(lastPosition);
                        }
                        break;
                    }
                    case "Player":
                        GameManager.Instance.ActualShipHealth -= 5;
                        RemoveEnemyAndScore(lastPosition);
                        break;
                    case "SpaceShipShield":
                        GameManager.Instance.ActualShieldHealth -= 5;
                        RemoveEnemyAndScore(lastPosition);
                        
                        break;
                }
                
                this.InCollision = false;
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
    
        private void RemoveEnemyAndScore(Vector3 lastPosition)
        {
            this.IAmDying = true;
            PlayEnemyExplosion();
            this.animator.SetBool(AmIDead, true);
            RemoveEnemyFromWave(enemyController.EnemyFlightFormations);
            Destroy(gameObject, 0.5f);
            GameManager.Instance.Score += GameManager.Instance.EnemyWaveThreeScore;
            
            enemyController.SpawnLoot(this.formationId,
                this.formation.enemyFormationData.LootTemplate, lastPosition);
        }
        
        private static void PlayEnemyExplosion()
        {
            var sound = AudioManager.Instance.GetSound("EnemyExplosion");
            sound.enabled = true;
            sound.Play();
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
