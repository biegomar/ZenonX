using UnityEngine;

namespace Enemies.Services
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
