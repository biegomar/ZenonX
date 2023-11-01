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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLIDE");
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
