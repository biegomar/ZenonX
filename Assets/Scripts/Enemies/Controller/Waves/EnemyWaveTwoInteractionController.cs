using System;
using System.Collections.Generic;
using System.Linq;
using Enemies.Model;
using Enemies.Services.Formations;
using UnityEngine;

namespace Enemies.Controller.Waves
{
    /// <summary>
    /// The interaction controller for enemy wave two.
    /// </summary>
    public class EnemyWaveTwoInteractionController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private WaveSpawnController enemyController;
        private EnemyFlightFormationItem enemyItem;
        private EnemyFormation formation;
        private Guid formationId;

        private const float rayLength = 9.7f;
        private const float translate = 0.6f;

        private RaycastHit2D leftHit;
        private RaycastHit2D rightHit;
        private Rigidbody2D rigidBody;
        private float timeSinceAppearance;

        private bool isInCollisionHanding = false;
        
        private static readonly int AmIDead = Animator.StringToHash("AmIDead");

        private void Start()
        {
            this.rigidBody = GetComponent<Rigidbody2D>();

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
        }

        private void Update()
        {
            if (GameManager.Instance.IsGameRunning && Time.deltaTime > 0f)
            {
                this.timeSinceAppearance += Time.deltaTime;

                var position = transform.position;
                var leftOrigin = new Vector3(position.x - translate, position.y, position.z);
                var rightOrigin = new Vector3(position.x + translate, position.y, position.z);
                leftHit = Physics2D.Raycast(leftOrigin + Vector3.down * .3f, Vector3.down, rayLength);
                rightHit = Physics2D.Raycast(rightOrigin + Vector3.down * .3f, Vector3.down, rayLength);

                LetTheHammerFall();
                RayDebugOutput();

                if (transform.position.y < -7)
                {
                    RemoveEnemyAndScore(false);
                }
            }            
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isInCollisionHanding && this.timeSinceAppearance > 1.0f)
            {
                var collisionObject = collision.gameObject;
                switch (collisionObject.tag)
                {
                    case "Player":
                    {
                        isInCollisionHanding = true;
                        GameManager.Instance.ActualShipHealth -= 5;
                        PlayEnemyExplosion();
                        RemoveEnemyAndScore();

                        break;
                    }
                    case "PlayerLaser":
                    {
                        if (enemyItem != null)
                        {
                            enemyItem.Health -= 1;
                            if (enemyItem.Health <= 0)
                            {
                                PlayEnemyExplosion();
                                RemoveEnemyAndScore();
                            }
                        }

                        Destroy(collisionObject);
                        break;
                    }
                    case "SpaceShipShield":
                    {
                        isInCollisionHanding = true;
                        GameManager.Instance.ActualShieldHealth -= 5;
                        PlayEnemyExplosion();
                        RemoveEnemyAndScore();

                        break;
                    }
                }
            }        
        }

        private void RemoveEnemyAndScore(bool reallyScore = true)
        {
            this.animator.SetBool(AmIDead, true);
            RemoveEnemyFromWave(enemyController.EnemyFlightFormations);
            Destroy(gameObject, 0.5f);

            if (reallyScore)
            {
                GameManager.Instance.Score += GameManager.Instance.EnemyWaveTwoScore;
            }
            
            enemyController.SpawnLoot(this.formationId,
                this.formation.enemyFormationData.LootTemplate, new Vector3(0,0,0));
        }
        
        private static void PlayEnemyExplosion()
        {
            var sound = AudioManager.Instance.GetSound("EnemyExplosion");
            sound.enabled = true;
            sound.Play();
        }

        private void LetTheHammerFall()
        {
            if ((leftHit.collider != null 
                 && (leftHit.collider.gameObject.CompareTag("Player") || leftHit.collider.gameObject.CompareTag("SpaceShipShield")))
                || (rightHit.collider != null 
                    && (rightHit.collider.gameObject.CompareTag("Player") || rightHit.collider.gameObject.CompareTag("SpaceShipShield"))))
            {
                this.rigidBody.gravityScale = 2.5f;
            }
        }

        private void RemoveEnemyFromWave(IDictionary<Guid, IList<EnemyFlightFormationItem>> EnemyWaves)
        {
            foreach (var wave in EnemyWaves)
            {
                var enemyFlightFormationItem = wave.Value.Where(item => item.Enemy == gameObject).FirstOrDefault();
                if (enemyFlightFormationItem != null)
                {
                    wave.Value.Remove(enemyFlightFormationItem);
                }
            }
        }

        private void RayDebugOutput()
        {
            var position = transform.position;
            var leftOrigin = new Vector3(position.x - translate, position.y, position.z);
            var rightOrigin = new Vector3(position.x + translate, position.y, position.z);
        
            Color debugColorLeft = leftHit.collider != null ? Color.green : Color.red;
            Debug.DrawRay(leftOrigin + Vector3.down * .3f, Vector2.down * rayLength, debugColorLeft, 0.01f, true);
        
            Color debugColorRight = rightHit.collider != null ? Color.green : Color.red;
            Debug.DrawRay(rightOrigin + Vector3.down * .3f, Vector2.down * rayLength, debugColorRight, 0.01f, true);
        }
    }
}
