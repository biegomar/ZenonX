using System;
using Enemies.Model;
using UnityEngine;

namespace Enemies.Services.Movements
{
    /// <summary>
    /// The cycloid movement.
    /// </summary>
    public class CycloidMovement : IMovementStrategy
    {
        private const float radius = .8f;
        private const float distanceFromOrigin = 3.0f;

        private float arc = 2.3f;
    
        private readonly Vector2 startPosition;
        private readonly EnemyFlightFormationItem item;

        public CycloidMovement(Vector2 initialPosition, EnemyFlightFormationItem item)
        {
            this.startPosition = initialPosition;
            this.item = item;            
        }

        public float CalculateNewXPosition(GameObject gameObject, bool isRightMovement)
        {
            if (this.item.Flag)
            {
                this.arc -= Time.deltaTime * GameManager.Instance.EnemyWaveFourSpeed;    
            }
            else
            {
                this.arc += Time.deltaTime * GameManager.Instance.EnemyWaveFourSpeed;    
            }
        
            var x = radius * arc - distanceFromOrigin * Mathf.Sin(arc);
        
            return this.startPosition.x + x; 
        }

        public float CalculateNewYPosition(GameObject gameObject, bool isUpMovement)
        {
            var y = radius - distanceFromOrigin * Mathf.Cos(arc);

            return this.startPosition.y + y;
        }
    }
}