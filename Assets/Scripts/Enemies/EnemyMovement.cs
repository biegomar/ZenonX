using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies.MovementStrategies;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector2 startPosition;
    private IMovementStrategy activeMovementStrategy;

    void Start()
    {     
        this.startPosition = transform.position;
        this.activeMovementStrategy = new SinusMovement(this.startPosition);
    }

    void Update()
    {               
        transform.position = new Vector3(
               CalculateNewXPosition(),
               CalculateNewYPosition(),
               transform.position.z);

        if (transform.position.y >= this.startPosition.y)
        {
            this.startPosition = transform.position;
            this.activeMovementStrategy = new SinusMovement(this.startPosition);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionObject = collision.gameObject;        
        if (collisionObject.tag == "PlayerLaser")
        {
            Destroy(collisionObject);

            GameObject go = GameObject.Find("Enemies");
            if (go != null) 
            {
                var enemyController = go.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    RemoveEnemyFromWave(enemyController.EnemyWaves);

                    var lastPosition = transform.position;
                    Destroy(gameObject);

                    enemyController.SpawnLoot(lastPosition);

                    GameManager.Score++;
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

        if (collisionObject.tag == "Border") 
        {            
            this.activeMovementStrategy = new DirectMovement(startPosition);
        }
    }

    private void RemoveEnemyFromWave(IDictionary<Guid, IList<GameObject>> EnemyWaves)
    {
        foreach (var wave in EnemyWaves)
        {
            if (wave.Value.Contains(gameObject))
            {
                wave.Value.Remove(gameObject);
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
