using UnityEngine;

namespace Enemies.Services.Movements
{
    public class XPingPongLerpMovement : IMovementStrategy
    {
        private readonly Vector2 targetPosition;
        private const float duration = 4.0f;

        public XPingPongLerpMovement(Vector2 targetPosition)
        {
            this.targetPosition = targetPosition;
        }

        public float CalculateNewXPosition(GameObject gameObject, bool isRightMovement)
        {
            return Vector2.Lerp(gameObject.transform.position, this.targetPosition, duration * Time.deltaTime).x;
        }

        public float CalculateNewYPosition(GameObject gameObject, bool isUpMovement)
        {
            return this.targetPosition.y;
        }
    }
}