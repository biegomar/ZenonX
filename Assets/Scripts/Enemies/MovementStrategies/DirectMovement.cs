using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemies.MovementStrategies
{
    internal class DirectMovement : IMovementStrategy
    {
        private Vector2 targetPosition;            

        public DirectMovement(Vector2 initialPosition, int count = 1)
        {
            DefineTargetPosition(initialPosition, count);            
        }

        private void DefineTargetPosition(Vector2 initialPosition, int count)
        {
            var factor = count % 2 == 0 ? 1 : -1;

            float newX = initialPosition.x switch
            {
                < 0 => initialPosition.x - (2f * factor),
                >= 0 => initialPosition.x + (2f * factor),
                _ => initialPosition.x,
            };

            this.targetPosition = new Vector2(newX, initialPosition.y + 0.01f);            
        }

        public float CalculateNewXPosition(GameObject gameObject)
        {
            var vec = Vector2.Lerp(gameObject.transform.position, this.targetPosition, GameManager.EnemyYSpeed * Time.deltaTime);

            return vec.x;
        }

        public float CalculateNewYPosition(GameObject gameObject)
        {
            var vec = Vector2.Lerp(gameObject.transform.position, this.targetPosition, GameManager.EnemyYSpeed * Time.deltaTime);

            return vec.y;
        }
    }
}
