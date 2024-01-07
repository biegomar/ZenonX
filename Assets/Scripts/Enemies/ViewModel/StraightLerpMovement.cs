using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enemies.MovementStrategies;
using UnityEngine;

namespace Assets.Scripts.Enemies.ViewModel
{
    internal class StraightLerpMovement : IMovementStrategy
    {
        private const float targetPositionY = 5.6f;        
        private const float duration = 2.3f;

        private Vector2 targetPosition;

        public StraightLerpMovement(Vector2 startPosition)
        {
            this.DefineTargetPosition(startPosition);
        }

        public float CalculateNewXPosition(GameObject gameObject)
        {                        
            return gameObject.transform.position.x;
        }

        public float CalculateNewYPosition(GameObject gameObject)
        {
            var vec = Vector2.Lerp(gameObject.transform.position, this.targetPosition, duration * Time.deltaTime);

            return vec.y;
        }

        private void DefineTargetPosition(Vector2 initialPosition)
        {            
            this.targetPosition = new Vector2(initialPosition.x, targetPositionY);
        }
    }
}
