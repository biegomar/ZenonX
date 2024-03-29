﻿using UnityEngine;

namespace Enemies.Services.Movements
{
    /// <summary>
    /// A simple direct movement.
    /// </summary>
    internal class DirectMovement : IMovementStrategy
    {
        private Vector2 startPosition;
        private Vector2 targetPosition;
        private float duration = 7f;
        private float elapsedTime;

        public DirectMovement(Vector2 initialPosition, GameObject gameObject, bool isNegativexDirection)
        {
            this.startPosition = gameObject.transform.position;
            DefineTargetPosition(initialPosition, isNegativexDirection);            
        }

        private void DefineTargetPosition(Vector2 initialPosition, bool isNegativexDirection)
        {
            var factor = isNegativexDirection ? 1 : -1;

            float newX = initialPosition.x switch
            {
                < 0 => initialPosition.x - (2f * factor),
                >= 0 => initialPosition.x + (2f * factor),
                _ => initialPosition.x,
            };

            this.targetPosition = new Vector2(newX, initialPosition.y);            
        }

        public float CalculateNewXPosition(GameObject gameObject, bool isRightMovement)
        {
            this.elapsedTime += Time.deltaTime;

            var vec = Vector2.Lerp(this.startPosition, this.targetPosition, this.elapsedTime / this.duration);

            return vec.x;
        }

        public float CalculateNewYPosition(GameObject gameObject, bool isUpMovement)
        {
            var vec = Vector2.Lerp(this.startPosition, this.targetPosition, this.elapsedTime / this.duration);

            return vec.y;
        }
    }
}