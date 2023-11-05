using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private const int sinusMultiplier = 2;
    private double activeSinus;
    private float initialX;    

    void Start()
    {
        this.activeSinus = 0d;
        this.initialX = transform.position.x;
    }

    void Update()
    {
        transform.position = new Vector3(
               CalculateNewXPosition(),
               CalculateNewYPosition(),
               transform.position.z);
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
        if (transform.position.y < 5) 
        {
            var result = this.initialX + (float)Math.Sin(activeSinus) * GameManager.EnemySinusAmplitude;

            if (activeSinus < Math.PI * sinusMultiplier)
            {
                activeSinus += GameManager.EnemySinusStep;
            }
            else
            {
                activeSinus = 0d;
            }

            return result;
        }

        return transform.position.x;        
    }

    private float CalculateNewYPosition()
    {        
        return transform.position.y - GameManager.EnemyYStep * GameManager.EnemyYSpeed;
    }
}
