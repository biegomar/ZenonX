using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.MovementStrategies;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{    
    private EnemyController enemyController;
    private Vector2 startPosition;
    private IMovementStrategy activeMovementStrategy;
    private const float hitInterval = 6f;
    private static float timeSinceLastHit = 0f;

    void Start()
    {
        GameObject go = GameObject.Find("Enemies");
        if (go != null)
        {
            this.enemyController = go.GetComponent<EnemyController>();
            if (this.enemyController == null)
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

    void Update()
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
        if (collisionObject.tag == "PlayerLaser")
        {
            var enemyItem = this.enemyController.Enemies[gameObject.GetInstanceID()];
            if (enemyItem != null)
            {
                enemyItem.Health = enemyItem.Health - 1;
                if (enemyItem.Health <= 0)
                {
                    RemoveEnemyFromWave(enemyController.EnemyWaves);

                    var lastPosition = transform.position;

                    Destroy(gameObject);

                    enemyController.SpawnLoot(lastPosition);

                    GameManager.Instance.Score++;
                }
            }

            Destroy(collisionObject);
        }

        if (collisionObject.tag == "Border") 
        {            
            this.activeMovementStrategy = new DirectMovement(startPosition, gameObject);
        }

        if (collisionObject.tag == "SpaceShip")
        {            
            Debug.Log($"timeSinceLastHit: {timeSinceLastHit} with enemy: {gameObject.GetInstanceID()}");

            if (timeSinceLastHit > hitInterval)
            {
                Debug.Log($"Hit SpaceShip, health points left: {GameManager.Instance.ActualShipHealth}");
                GameManager.Instance.ActualShipHealth -= 5;
                timeSinceLastHit = 0f;
            }            
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

    private void TryToSwitchToSinusMovement()
    {
        GameObject go = GameObject.Find("Enemies");
        if (go != null)
        {
            var enemyController = go.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                var waveId = enemyController.EnemyWaves.Values.SelectMany(x => x).Where(x => x.Enemy == gameObject).Select(x => x.WaveId).SingleOrDefault();

                if (waveId != null && !enemyController.EnemyWaves[waveId].Where(x => x.StartPosition.y != x.Enemy.transform.position.y).Any())
                {
                    this.startPosition = transform.position;
                    this.activeMovementStrategy = new SinusMovement(this.startPosition);
                }
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

    private float CalculateNewXPosition()
    {
        return this.activeMovementStrategy.CalculateNewXPosition(gameObject);        
    }

    private float CalculateNewYPosition()
    {        
        return this.activeMovementStrategy.CalculateNewYPosition(gameObject);
    }
}
