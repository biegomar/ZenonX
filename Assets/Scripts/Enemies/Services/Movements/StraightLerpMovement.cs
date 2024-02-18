﻿using UnityEngine;

namespace Enemies.Services.Movements
{
    internal class StraightLerpMovement : IMovementStrategy
    {
        private const float targetPositionY = 2.5f;        
        private const float duration = 2.3f;

        private Vector2 targetPosition;

        public StraightLerpMovement(Vector2 startPosition)
        {
            this.DefineTargetPosition(startPosition);
        }

        public float CalculateNewXPosition(GameObject gameObject, bool isRightMovement)
        {
            return gameObject.transform.position.x;
        }

        public float CalculateNewYPosition(GameObject gameObject, bool isUpMovement)
        {
            return Vector2.Lerp(gameObject.transform.position, this.targetPosition, duration * Time.deltaTime).y;
        }

        private void DefineTargetPosition(Vector2 initialPosition)
        {            
            this.targetPosition = new Vector2(initialPosition.x, targetPositionY);
        }
    }
}