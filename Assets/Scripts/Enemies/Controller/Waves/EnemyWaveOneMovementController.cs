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
    /// The movement controller for enemy wave one.
    /// </summary>
    public class EnemyWaveOneMovementController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private const float hitInterval = 6f;
        private float timeSinceLastHit = hitInterval;

        private WaveSpawnController enemyController;
        private EnemyFlightFormationItem enemyItem;
        private EnemyFormation formation;
        private Guid formationId;
        private Vector2 startPosition;
        private IMovementStrategy activeMovementStrategy;    

        private bool isSinusWaveYDirectionPositiv;
        private static readonly int AmIDead = Animator.StringToHash("AmIDead");
        private bool IAmDying;

        void Start()
        {
            this.IAmDying = false;
            
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
                    if (enemyItem != null && !this.IAmDying)
                    {
                        Destroy(collisionObject);
                        enemyItem.Health -= 1;
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
                    if (timeSinceLastHit > hitInterval && !this.IAmDying)
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
                    if (timeSinceLastHit > hitInterval && !this.IAmDying)
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
            this.IAmDying = true;
            PlayEnemyExplosion();
            this.animator.SetBool(AmIDead, true);
            RemoveEnemyFromWave(enemyController.EnemyFlightFormations);
            Destroy(gameObject, 0.5f);
            GameManager.Instance.Score += GameManager.Instance.EnemyWaveOneScore;
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
            return this.activeMovementStrategy.CalculateNewXPosition(gameObject, this.isSinusWaveYDirectionPositiv);        
        }

        private float CalculateNewYPosition()
        {        
            return this.activeMovementStrategy.CalculateNewYPosition(gameObject, this.isSinusWaveYDirectionPositiv);
        }
    }
}
