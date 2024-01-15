using System;
using UnityEngine;

public class CycloidMovement : IMovementStrategy
{
    private const float radius = 1.0f;
    private const float distanceFromOrigin = 3.0f;

    private float arc = 2.3f;
    
    private readonly Vector2 startPosition;
    private readonly bool isNegative;

    public CycloidMovement(Vector2 initialPosition, bool isNegative=false)
    {
        this.startPosition = initialPosition;
        this.isNegative = isNegative;
    }
        
    public float CalculateNewXPosition(GameObject gameObject)
    {
        if (this.isNegative)
        {
            this.arc -= Time.deltaTime * GameManager.Instance.EnemyWaveFourSpeed;    
        }
        else
        {
            this.arc += Time.deltaTime * GameManager.Instance.EnemyWaveFourSpeed;    
        }
        
        var x = radius * arc - distanceFromOrigin * Math.Sin(arc);
        
        return this.startPosition.x + (float)x; 
    }

    public float CalculateNewYPosition(GameObject gameObject)
    {
        var y = radius - distanceFromOrigin * Math.Cos(arc);

        return this.startPosition.y + (float)y;
    }
}