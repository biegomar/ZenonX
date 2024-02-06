using UnityEngine;

namespace Enemies.Services.Movements
{
    internal interface IMovementStrategy
    {
        float CalculateNewXPosition(GameObject gameObject, bool isRightMovement);

        float CalculateNewXPosition(GameObject gameObject)
        {
            return this.CalculateNewXPosition(gameObject, true);
        }
        
        float CalculateNewYPosition(GameObject gameObject, bool isUpMovement);

        float CalculateNewYPosition(GameObject gameObject)
        {
            return this.CalculateNewYPosition(gameObject, true);
        }
    }
}
