using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyWaveTwoInteractionController : MonoBehaviour
{
    private EnemyWaveTwoSpawnController enemyController;
    private EnemyItem enemyItem;

    private const float rayLength = 9.7f;

    private RaycastHit2D hit;
    private Rigidbody2D rigidBody;
    private float timeSinceAppearance;

    private bool isInCollisionHanding = false;

    private void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();

        GameObject go = GameObject.Find("EnemyWaveTwo");
        if (go != null)
        {
            this.enemyController = go.GetComponent<EnemyWaveTwoSpawnController>();
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
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameRunning)
        {
            this.timeSinceAppearance += Time.deltaTime;

            hit = Physics2D.Raycast(transform.position + Vector3.down * .3f, Vector3.down, rayLength);

            LetTheHammerFall();
            RayDebugOutput();
        }            
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInCollisionHanding && this.timeSinceAppearance > 1.0f)
        {
            var collisionObject = collision.gameObject;
            switch (collisionObject.tag)
            {
                case "SpaceShip":
                    {
                        isInCollisionHanding = true;
                        GameManager.Instance.ActualShipHealth -= 5;

                        Destroy(gameObject);
                        break;
                    }
                case "PlayerLaser":
                    {
                        if (enemyItem != null)
                        {
                            enemyItem.Health = enemyItem.Health - 1;
                            if (enemyItem.Health <= 0)
                            {
                                RemoveEnemyFromWave(enemyController.EnemyFlightFormation);

                                Destroy(gameObject);

                                GameManager.Instance.Score++;
                            }
                        }

                        Destroy(collisionObject);
                        break;
                    }
            }
        }        
    }

    private void LetTheHammerFall()
    {
        if (hit.collider != null && hit.collider.gameObject.tag == "SpaceShip")
        {
            this.rigidBody.gravityScale = 2;
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
        Color debugColor = hit.collider != null ? Color.green : Color.red;
        Debug.DrawRay(transform.position + Vector3.down * .3f, Vector2.down * rayLength, debugColor, 0.01f, true);
    }
}
