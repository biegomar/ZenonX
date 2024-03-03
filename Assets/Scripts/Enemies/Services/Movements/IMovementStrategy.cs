using UnityEngine;

namespace Enemies.Services.Movements
{
    /// <summary>
    /// The base interface for movement strategies.
    /// </summary>
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
