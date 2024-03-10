using UnityEngine;

namespace Enemies.Services.Movements
{
    /// <summary>
    /// A ping pong lerp movement on the x axis.
    /// </summary>
    public class XPingPongLerpMovement : IMovementStrategy
    {
        private readonly Vector2 targetPosition;
        private const float speed = 4.0f;

        public XPingPongLerpMovement(Vector2 targetPosition)
        {
            this.targetPosition = targetPosition;
        }

        public float CalculateNewXPosition(GameObject gameObject, bool isRightMovement)
        {
            var blend = Mathf.Pow(0.5f, speed * Time.deltaTime);
            return Vector2.Lerp(gameObject.transform.position, this.targetPosition, blend).x;
        }

        public float CalculateNewYPosition(GameObject gameObject, bool isUpMovement)
        {
            return this.targetPosition.y;
        }
    }
}