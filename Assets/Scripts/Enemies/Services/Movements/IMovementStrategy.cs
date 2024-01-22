using UnityEngine;

namespace Enemies.Services.Movements
{
    internal interface IMovementStrategy
    {
        float CalculateNewXPosition(GameObject gameObject);
        float CalculateNewYPosition(GameObject gameObject);
    }
}
